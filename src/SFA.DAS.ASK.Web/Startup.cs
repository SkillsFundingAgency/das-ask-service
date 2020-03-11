using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SFA.DAS.ASK.Application.ExternalServices;
using SFA.DAS.ASK.Application.ExternalServices.DfeSignInApi;
using SFA.DAS.ASK.Application.ExternalServices.ReferenceDataApi;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Application.Services.Email;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.Infrastructure;
using SFA.DAS.ASK.Web.Infrastructure.Filters;
using SFA.DAS.ASK.Web.Infrastructure.NServiceBus;
using SFA.DAS.Boilerplate.Configuration;
using SFA.DAS.Boilerplate.Logging;

namespace SFA.DAS.ASK.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddNLogLogging(Configuration, "das-ask-service-web");
            
            var config = new ConfigurationBuilder()
                .AddConfiguration(Configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddAzureStorageConfigurationProvider("SFA.DAS.Ask", "1.0").Build();

            Configuration = config;
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = ".Ask.Cookies";
                    options.Cookie.HttpOnly = true;

                    if (!_environment.IsDevelopment())
                    {
                        options.Cookie.Domain = ".apprenticeships.education.gov.uk";
                    }

                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {

                    options.SignInScheme = "Cookies";
                    options.Authority = Configuration["DfeSignIn:MetadataAddress"];
                    options.RequireHttpsMetadata = false;
                    options.ClientId = Configuration["DfeSignIn:ClientId"];
                    options.ClientSecret = Configuration["DfeSignIn:ClientSecret"];
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    
                    
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("email");
                    options.Scope.Add("profile");

                    options.SaveTokens = true;
                    options.UseTokenLifetime = true;
                    
                    options.SecurityTokenValidator = new JwtSecurityTokenHandler
                    {
                        InboundClaimTypeMap = new Dictionary<string, string>(),
                        TokenLifetimeInMinutes = 20,
                        SetDefaultTimesOnTokenCreation = true,
                    };
                    options.ProtocolValidator = new OpenIdConnectProtocolValidator
                    {
                        RequireSub = true,
                        RequireStateValidation = false,
                        NonceLifetime = TimeSpan.FromMinutes(15)
                    };
                    
                    options.DisableTelemetry = true;
                    options.Events = new OpenIdConnectEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var isSpuriousAuthCbRequest =
                                context.Request.Path == options.CallbackPath &&
                                context.Request.Method == "GET" &&
                                !context.Request.Query.ContainsKey("code");

                            if (isSpuriousAuthCbRequest)
                            {
                                context.HandleResponse();
                                context.Response.StatusCode = 302;
                                context.Response.Headers["Location"] = "/";
                            }

                            return Task.CompletedTask;
                        },

                        OnRemoteFailure = ctx =>
                        {
                            return Task.FromResult(0);
                        },

                        OnTokenValidated = context =>
                        {
                            var a = context;
                            return Task.CompletedTask;
                        }
                    };
                });

            if (!_environment.IsDevelopment())
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = Configuration["SessionRedisConnectionString"];
                    options.InstanceName = "das_ask_";
                });
            }

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddOptions();
            services.Configure<ReferenceDataApiConfig>(Configuration.GetSection("ReferenceDataApiAuthentication"));
            services.Configure<DfeSignInConfig>(Configuration.GetSection("DfeSignIn"));
            services.Configure<NServiceBusConfiguration>(Configuration.GetSection("NServiceBusConfiguration"));

            services.AddScoped<CheckRequestFilter>();

            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>();
            services.AddHttpClient<IDfeSignInApiClient, DfeSignInApiClient>();
            
            services.AddAuthorization();

            services.AddHealthChecks();

            services.AddMediatR(typeof(StartTempSupportRequestHandler));
            
            services.AddTransient<DbConnection>(provider => new SqlConnection(Configuration["SqlConnectionstring"]));
            services.AddDbContext<AskContext>(options => options.UseSqlServer(Configuration["SqlConnectionstring"]));
            services.AddMvc().AddSessionStateTempDataProvider().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddNServiceBus();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
