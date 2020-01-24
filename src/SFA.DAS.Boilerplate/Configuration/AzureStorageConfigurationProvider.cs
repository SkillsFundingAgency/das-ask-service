using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SFA.DAS.Assessor.Functions.Infrastructure;

namespace SFA.DAS.Boilerplate.Configuration
{
    public class AzureStorageConfigurationProvider : ConfigurationProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _appname;
        private readonly string _version;

        public AzureStorageConfigurationProvider(IConfiguration configuration, string appname, string version)
        {
            _configuration = configuration;
            _appname = appname;
            _version = version;
        }

        public override void Load()
        {
            if (string.IsNullOrWhiteSpace(_configuration["ConfigurationStorageConnectionString"]))
            {
                return;
            }
            
            var table = GetTable();
            var operation = GetOperation(_appname, _configuration["EnvironmentName"], _version);
            var result = table.ExecuteAsync(operation).Result;

            var configItem = (ConfigurationItem)result.Result;

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
            return TableOperation.Retrieve<ConfigurationItem>(environmentName, $"{serviceName}_{version}");
        }
    }
}