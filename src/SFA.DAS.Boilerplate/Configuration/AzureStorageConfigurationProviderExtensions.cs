using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;

namespace SFA.DAS.Boilerplate.Configuration
{
    public static class AzureStorageConfigurationProviderExtensions
    {
        public static IConfigurationBuilder AddAzureStorageConfigurationProvider(this IConfigurationBuilder builder, string appname, string version, Logger logger)
        {
            // builder.SetBasePath(Directory.GetCurrentDirectory());
            // builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            // builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            var config = builder.Build();
            
            return builder.Add(new AzureStorageConfigurationSource(config, appname, version, logger));
        }

        // public static IConfigurationRoot AddAzureStorageOptions(this IFunctionsHostBuilder builder, string appname, string version)
        // {
        //     var tempConfig = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("local.settings.json", true).Build();
        //
        //     var config = new ConfigurationBuilder()
        //         .AddConfiguration(tempConfig)
        //         .Add(new AzureStorageConfigurationSource(tempConfig, appname, version)).Build();
        //     
        //     return config;
        // }
    }
}