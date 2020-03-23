using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ASK.Application.ExternalServices.DfeSignInApi;
using SFA.DAS.ASK.Application.ExternalServices.ReferenceDataApi;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.Email;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
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

        private IConfiguration Configuration { get; set; }

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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AskContext>(options => options.UseSqlServer(Configuration["SqlConnectionstring"]));
            services.AddMediatR(typeof(StartTempSupportRequestHandler));
            
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            // services.AddDfeAuthentication(_environment, Configuration)
            //     .AddAuthorization();

            if (!_environment.IsDevelopment())
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = Configuration["SessionRedisConnectionString"];
                    options.InstanceName = "das_ask_";
                });
            }
            
            services.AddOptions()
                .Configure<ReferenceDataApiConfig>(Configuration.GetSection("ReferenceDataApiAuthentication"))
                .Configure<DfeSignInConfig>(Configuration.GetSection("DfeSignIn"));

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            
            
            services.Configure<NServiceBusConfiguration>(Configuration.GetSection("NServiceBusConfiguration"));

            
            services.AddTransient<IEmailService, EmailService>();
            
            services.AddScoped<CheckRequestFilter>()
                .AddTransient<ISessionService, SessionService>();
            
            services.AddHttpClients();
            services.AddAuthorization();

            services.AddHealthChecks();

            services.AddMvc()
                .AddSessionStateTempDataProvider()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddTransient<DbConnection>(provider => new SqlConnection(Configuration["SqlConnectionstring"]));
            
            
            services.AddNServiceBus();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
