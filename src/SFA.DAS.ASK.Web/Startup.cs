using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
using Microsoft.IdentityModel.Logging;
using SFA.DAS.ASK.Application.DfeApi;
using SFA.DAS.ASK.Application.Handlers.RequestSupport;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest;
using SFA.DAS.ASK.Data;
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = ".Assessors.Cookies";
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
                    options.CorrelationCookie = new CookieBuilder()
                    {
                        Name = ".Assessors.Correlation.",
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                        SecurePolicy = CookieSecurePolicy.SameAsRequest
                    };

                    options.SignInScheme = "Cookies";
                    options.Authority = Configuration["DfeSignIn:MetadataAddress"];
                    options.RequireHttpsMetadata = false;
                    options.ClientId = Configuration["DfeSignIn:ClientId"];

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");

                    options.SaveTokens = true;

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

                        OnTokenValidated = async context =>
                        {
                            var a = context;
                        }
                    };
                });


            services.AddHttpClient<IDfeSignInApiClient, DfeSignInApiClient>(client => client.BaseAddress = new Uri(Configuration["DfeSignIn:ApiUri"]));
            
            services.AddAuthorization();
            
            services.AddNLogLogging(Configuration);

            services.AddHealthChecks();

            services.AddApplicationInsightsTelemetry();

            services.AddMediatR(typeof(StartRequestHandler));

            services.AddDbContext<RequestSupportContext>(options => options.UseInMemoryDatabase("SFA.DAS.ASK.Web"));
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
