using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SFA.DAS.Boilerplate.Configuration;

namespace SFA.DAS.ASK.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddAzureStorageConfigurationProvider(context, "SFA.DAS.ASK.Web", "1.0");
                })
                .UseStartup<Startup>();
    }
}
