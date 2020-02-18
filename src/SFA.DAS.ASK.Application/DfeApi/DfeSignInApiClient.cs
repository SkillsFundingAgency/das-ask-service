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
            //return new List<DfeOrganisation>();
            
            return new List<DfeOrganisation>{new DfeOrganisation
            {
                Address = "3 The Street\r\nThe Village\r\nTheTown\r\nStaffordshire\r\nWS12 3UP",
                Name = "Coventry High School",
                UkPrn = 10000323,
                Urn = "3445678",
                Telephone = "07777 377366"
            }};

            // return new List<DfeOrganisation>
            // {
            //     new DfeOrganisation
            //     {
            //         Address = "3 The Street\r\nThe Village\r\nTheTown\r\nStaffordshire\r\nWS12 3UP",
            //         Name = "Coventry High School",
            //         UkPrn = 10000323,
            //         Urn = "3445678",
            //         Telephone = "07777 377366"
            //     },
            //     new DfeOrganisation
            //     {
            //         Address = "3 The Street\r\nThe Village\r\nTheTown\r\nStaffordshire\r\nWS12 3UP",
            //         Name = "Kingsmead Academy",
            //         UkPrn = 10000888,
            //         Urn = "9876543",
            //         Telephone = "01543 344255"
            //     }
            // };
        }
    }
}