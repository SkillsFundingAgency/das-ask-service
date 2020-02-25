using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SFA.DAS.ASK.Application.Services.ReferenceData;

namespace SFA.DAS.ASK.Application.ExternalServices.ReferenceDataApi
{
    public class ReferenceDataApiClient : IReferenceDataApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ReferenceDataApiConfig _config;

        public ReferenceDataApiClient(HttpClient httpClient, IOptions<ReferenceDataApiConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
            
            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
            
        }
        
        public async Task<IEnumerable<ReferenceDataSearchResult>> Search(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"?searchTerm={searchTerm}");
            var results = await response.Content.ReadAsAsync<IEnumerable<ReferenceDataSearchResult>>();
            return results;
        }

        private string GetToken()
        {
            var authority = $"https://login.microsoftonline.com/{_config.TenantId}";
            var clientCredential = new ClientCredential(_config.ClientId, _config.ClientSecret);
            var context = new AuthenticationContext(authority, true);
            var result = context.AcquireTokenAsync(_config.Audience, clientCredential).Result;

            return result.AccessToken;
        }
    }
}