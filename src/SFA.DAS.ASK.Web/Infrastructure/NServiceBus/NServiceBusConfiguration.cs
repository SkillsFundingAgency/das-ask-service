namespace SFA.DAS.ASK.Web.Infrastructure.NServiceBus
{
    public class NServiceBusConfiguration
    {
        public string ServiceBusConnectionString { get; set; }
        public string SharedServiceBusEndpointUrl { get; set; }
        public string NServiceBusLicense
        {
            get => _decodedNServiceBusLicense ??
                   (_decodedNServiceBusLicense = System.Net.WebUtility.HtmlDecode(_nServiceBusLicense));
            set => _nServiceBusLicense = value;
        }

        private string _nServiceBusLicense;
        private string _decodedNServiceBusLicense;
    }
}