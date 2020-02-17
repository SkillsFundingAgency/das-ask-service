using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SFA.DAS.Boilerplate.Configuration;

namespace SFA.DAS.ASK.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // .ConfigureAppConfiguration((context, builder) =>
                // {
                //     builder.AddAzureStorageConfigurationProvider(context, "SFA.DAS.ASK.Api", "1.0", null);
                // })
                .UseStartup<Startup>();
    }
}