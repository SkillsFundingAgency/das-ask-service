using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NServiceBus;
using SFA.DAS.Notifications.Messages.Commands;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using SFA.DAS.NServiceBus.Configuration.NLog;
using SFA.DAS.NServiceBus.Hosting;
using SFA.DAS.NServiceBus.SqlServer.Configuration;
using SFA.DAS.UnitOfWork.NServiceBus.Configuration;

namespace SFA.DAS.ASK.Web.Infrastructure.NServiceBus
{
    public static class ServiceCollectionExtensions
    {
        private const string EndpointName = "SFA.DAS.ASK.Web";

        public static IServiceCollection AddNServiceBus(this IServiceCollection services)
        {
            return services
                .AddSingleton(p =>
                {
                    var sp = services.BuildServiceProvider();
                    var configuration = sp.GetService<IOptions<NServiceBusConfiguration>>().Value;
                    
                    var hostingEnvironment = p.GetService<IHostingEnvironment>();
                    //
                    // var runInDevelopmentMode = hostingEnvironment.IsDevelopment() || hostingEnvironment.EnvironmentName == Domain.Constants.IntegrationTestEnvironment;

                    var endpointConfiguration = new EndpointConfiguration(EndpointName)
                        .UseErrorQueue($"{EndpointName}-errors")
                        .UseInstallers()
                        .UseLicense(configuration.NServiceBusLicense)
                        .UseMessageConventions()
                        .UseNewtonsoftJsonSerializer()
                        .UseNLogFactory()
                        .UseOutbox()
                        .UseSqlServerPersistence(() => sp.GetService<DbConnection>())
                        .UseUnitOfWork();

                    // if (runInDevelopmentMode)
                    // {
                    //     endpointConfiguration.UseLearningTransport(s => s.AddRouting());
                    // }
                    // else
                    // {
                        endpointConfiguration.UseAzureServiceBusTransport(configuration.SharedServiceBusEndpointUrl, s => s.AddRouting());
                    //}
                    
                    var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

                    return endpoint;
                })
                .AddSingleton<IMessageSession>(s => s.GetService<IEndpointInstance>())
                .AddHostedService<NServiceBusHostedService>();
        }
    }
    
    public static class RoutingSettingsExtensions
    {
        private const string NotificationsMessageHandler = "SFA.DAS.Notifications.MessageHandlers";

        public static void AddRouting(this RoutingSettings routingSettings)
        {
            routingSettings.RouteToEndpoint(typeof(SendEmailCommand), NotificationsMessageHandler);
        }
    }
}