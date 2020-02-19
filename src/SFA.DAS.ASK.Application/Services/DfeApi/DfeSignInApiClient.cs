using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace SFA.DAS.ASK.Application.Services.DfeApi
{
    public class DfeSignInApiClient : IDfeSignInApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly DfeSignInConfig _config;

        public DfeSignInApiClient(HttpClient httpClient, IOptions<DfeSignInConfig> config)
        {
            _config = config.Value;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_config.ApiUri);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
        }

        private string GetToken()
        {
            var mySecret = _config.ApiClientSecret;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = _config.ClientId;
            var myAudience = "signin.education.gov.uk";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        public async Task<List<DfeOrganisation>> GetOrganisations(Guid requestDfeSignInId)
        {
            var response = await _httpClient.GetAsync($"users/{requestDfeSignInId}/organisations");

            return await response.Content.ReadAsAsync<List<DfeOrganisation>>();
            
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