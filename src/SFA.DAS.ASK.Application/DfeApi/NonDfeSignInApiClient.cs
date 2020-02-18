using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace SFA.DAS.ASK.Application.DfeApi
{
    public class NonDfeSignInApiClient : INonDfeSignInApiClient
    {
        private readonly HttpClient _httpClient;

        public NonDfeSignInApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<NonDfeOrganisation> GetOrganisations()
        {
            return new List<NonDfeOrganisation>
            {
                new NonDfeOrganisation
                {
                    Name = "Test School",
                    Type = 4,
                    SubType = 0,
                    Code = "111111",
                    RegistrationDate = null,
                    Address = new NonDfeAddress { Line1 = "Test Road", Line2 = null, Line3 = "Test", Line4 = "Test", Line5 = "City", Postcode="AA1 2BB"},
                    Sector = "Test School Type"
                },
                new NonDfeOrganisation
                {
                    Name = "Test School Two",
                    Code = "111112",
                    Type = 4,
                    SubType = 0,
                    RegistrationDate = null,
                    Address = new NonDfeAddress { Line1 = "Test Road", Line2 = null, Line3 = "Test", Line4 = "Test", Line5 = "City", Postcode="AA1 2BB"},
                    Sector = "Test School Type"
                }
            };
        }
    }
}
