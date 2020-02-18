using Microsoft.Extensions.Configuration;
using NLog;

namespace SFA.DAS.Boilerplate.Configuration
{
    public class AzureStorageConfigurationSource : IConfigurationSource
    {
        private readonly IConfiguration _configuration;
        private readonly string _appname;
        private readonly string _version;
        private readonly Logger _logger;

        public AzureStorageConfigurationSource(IConfiguration configuration, string appname, string version, Logger logger)
        {
            _configuration = configuration;
            _appname = appname;
            _version = version;
            _logger = logger;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AzureStorageConfigurationProvider(_configuration, _appname, _version, _logger);
        }
    }
}