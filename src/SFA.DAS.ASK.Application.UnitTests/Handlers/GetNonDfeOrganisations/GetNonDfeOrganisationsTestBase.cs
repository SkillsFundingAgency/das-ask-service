using MediatR;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.GetNonDfeOrganisations
{
    [TestFixture]
    public class GetNonDfeOrganisationsTestBase
    {


        public GetNonDfeOrganisationsHandler Handler { get; set; }

        public ISessionService sessionService { get; set; }
        public IReferenceDataApiClient referenceDataApi { get; set; }

        private Guid requestId = Guid.Parse("63be476e-0593-40c5-9b8d-8f0358a4d195");

        [SetUp]
        public void Arrange()
        {           
            sessionService = Substitute.For<ISessionService>();
            referenceDataApi = Substitute.For<IReferenceDataApiClient>();

            referenceDataApi.Search("Test School").Returns(Task.FromResult<IEnumerable<ReferenceDataSearchResult>>(new List<ReferenceDataSearchResult>() { new ReferenceDataSearchResult { Name = "Test Result" } }));             //new Task<IEnumerable<ReferenceDataSearchResult>>());
            sessionService.Get(Arg.Any<string>()).Returns(GetCachedSearchResults());


            Handler = new GetNonDfeOrganisationsHandler(referenceDataApi, sessionService);
        }

        private string GetCachedSearchResults()
        {
            return "[{\"Name\": \"Test School\"}]";
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
