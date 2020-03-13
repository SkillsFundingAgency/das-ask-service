using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetNonDfeOrganisations
{
    [TestFixture]
    public class GetNonDfeOrganisationsTestBase
    {
        public GetNonDfeOrganisationsHandler Handler { get; set; }

        protected ISessionService SessionService;
        protected IReferenceDataApiClient ReferenceDataApi;

        private Guid requestId = Guid.Parse("63be476e-0593-40c5-9b8d-8f0358a4d195");

        [SetUp]
        public void Arrange()
        {           
            SessionService = Substitute.For<ISessionService>();
            ReferenceDataApi = Substitute.For<IReferenceDataApiClient>();

            ReferenceDataApi.Search("Test School").Returns(Task.FromResult<IEnumerable<ReferenceDataSearchResult>>(GetSearchResults()));            
            
            Handler = new GetNonDfeOrganisationsHandler(ReferenceDataApi, SessionService);
        }

        public List<ReferenceDataSearchResult> GetCachedSearchResults()
        {
            return new List<ReferenceDataSearchResult>() {
                new ReferenceDataSearchResult {
                    Name = "Test School",
                    Address = new ReferenceDataAddress { Line1 = "School Road"}
                }
            };
        }

        private List<ReferenceDataSearchResult> GetSearchResults()
        {
            return new List<ReferenceDataSearchResult>() { 
                new ReferenceDataSearchResult { 
                    Name = "Test School Search Result",
                    Address = new ReferenceDataAddress { Line1 = "School Road"}
                } 
            };
        }
    }
}
