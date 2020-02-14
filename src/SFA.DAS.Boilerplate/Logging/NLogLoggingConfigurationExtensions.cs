using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace SFA.DAS.Boilerplate.Logging
{
    public static class NLogLoggingConfigurationExtensions
    {
        public static IServiceCollection AddNLogLogging(this IServiceCollection services, IConfiguration configuration, string appName)
        {
            var nLogConfiguration = new NLogConfiguration();

            services.AddLogging((options) =>
            {
                options.SetMinimumLevel(LogLevel.Trace);
                options.SetMinimumLevel(LogLevel.Trace);
                options.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
                options.AddConsole();

                nLogConfiguration.ConfigureNLog(configuration, appName);
            });

            return services;
        }
    }
}