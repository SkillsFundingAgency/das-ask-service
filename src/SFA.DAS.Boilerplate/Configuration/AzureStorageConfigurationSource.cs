using Microsoft.Extensions.Configuration;

namespace SFA.DAS.Boilerplate.Configuration
{
    public class AzureStorageConfigurationSource : IConfigurationSource
    {
        private readonly IConfiguration _configuration;
        private readonly string _appname;
        private readonly string _version;

        public AzureStorageConfigurationSource(IConfiguration configuration, string appname, string version)
        {
            _configuration = configuration;
            _appname = appname;
            _version = version;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AzureStorageConfigurationProvider(_configuration, _appname, _version);
        }
    }
}