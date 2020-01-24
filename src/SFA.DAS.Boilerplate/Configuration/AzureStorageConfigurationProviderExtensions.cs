using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SFA.DAS.Boilerplate.Configuration
{
    public static class AzureStorageConfigurationProviderExtensions
    {
        public static IConfigurationBuilder AddAzureStorageConfigurationProvider(this IConfigurationBuilder builder, WebHostBuilderContext context, string appname, string version)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            var config = builder.Build();
            
            return builder.Add(new AzureStorageConfigurationSource(config, appname, version));
        }
    }
}