using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SFA.DAS.ASK.Application.DfeApi
{
    public class DfeSignInApiClient : IDfeSignInApiClient 
    {
        private readonly HttpClient _httpClient;

        public DfeSignInApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<DfeOrganisation> GetOrganisations(Guid requestDfeSignInId)
        {
            return new List<DfeOrganisation>
            {new DfeOrganisation
            {
                Address = "3 The Street\r\nThe Village\r\nTheTown\r\nCounty\r\nStaffordshire\r\nWS12 3UP",
                Name = "Coventry High School",
                UkPrn = 10000323
            }};
        }
    }
}