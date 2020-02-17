using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SFA.DAS.Assessor.Functions.Infrastructure;

namespace SFA.DAS.Boilerplate.Configuration
{
    public class AzureStorageConfigurationProvider : ConfigurationProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _appname;
        private readonly string _version;
        private readonly ILogger _logger;

        public AzureStorageConfigurationProvider(IConfiguration configuration, string appname, string version, ILogger logger)
        {
            _configuration = configuration;
            _appname = appname;
            _version = version;
            _logger = logger;
        }

        public override void Load()
        {
            _logger.LogInformation("AzureStorageConfigurationProvider Load()");
            if (string.IsNullOrWhiteSpace(_configuration["ConfigurationStorageConnectionString"]))
            {
                return;
            }
            _logger.LogInformation($"AzureStorageConfigurationProvider Got ConfigurationStorageConnectionString of {_configuration["ConfigurationStorageConnectionString"].Substring(0,20)}");
            
            var table = GetTable();
            
            var operation = GetOperation(_appname, _configuration["EnvironmentName"], _version);
            var result = table.Execute(operation);

            _logger.LogInformation($"AzureStorageConfigurationProvider Get Table result: {result.HttpStatusCode}");
            
            var configItem = (ConfigurationItem)result.Result;
            
            _logger.LogInformation($"AzureStorageConfigurationProvider Table Data: {configItem.Data}");

            var jsonObject = JObject.Parse(configItem.Data);

            foreach (var child in jsonObject.Children())
            {
                if (child.Type == JTokenType.Property)
                {
                    Data.Add($"{child.Path}", ((JProperty)child).Value.ToString());
                }
                else
                {
                    foreach (var jToken in child.Children().Children())
                    {
                        var child1 = (JProperty)jToken;
                        Data.Add($"{child.Path}:{child1.Name}", child1.Value.ToString());
                    }
                }
            }
        }

        private CloudTable GetTable()
        {
            var storageAccount = CloudStorageAccount.Parse(_configuration["ConfigurationStorageConnectionString"]);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference("Configuration");
        }

        private TableOperation GetOperation(string serviceName, string environmentName, string version)
        {
            _logger.LogInformation($"AzureStorageConfigurationProvider TableOperation.Retrieve<ConfigurationItem>({environmentName}, {serviceName}_{version}");
            return TableOperation.Retrieve<ConfigurationItem>(environmentName, $"{serviceName}_{version}");
        }
    }
}