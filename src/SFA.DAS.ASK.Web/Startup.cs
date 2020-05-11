using System.IO;
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
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Web.Infrastructure.Authentication;
using SFA.DAS.ASK.Web.Infrastructure.Filters;
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

            //services.AddDbContext<AskContext>(options => options.UseInMemoryDatabase("SFA.DAS.ASK.Web"));
            services.AddDbContext<AskContext>(options => options.UseSqlServer(Configuration["SqlConnectionstring"]));
            services.AddMediatR(typeof(StartTempSupportRequestHandler));
            
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            services.AddDfeAuthentication(_environment, Configuration);

            services.AddAuthorization();

            if (!_environment.IsDevelopment())
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = Configuration["SessionRedisConnectionString"];
                    options.InstanceName = "das_ask_";
                });
            }

            

            services.AddOptions();
            services.Configure<ReferenceDataApiConfig>(Configuration.GetSection("ReferenceDataApiAuthentication"));
            services.Configure<DfeSignInConfig>(Configuration.GetSection("DfeSignIn"));
            
            services.AddScoped<CheckRequestFilter>();

            services.AddTransient<ISessionService, SessionService>();

            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>();
            services.AddHttpClient<IDfeSignInApiClient, DfeSignInApiClient>();
            
            
            
            services.AddHealthChecks();

            services.AddMvc().AddSessionStateTempDataProvider().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
