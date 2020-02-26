using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSelectedOrganisationSearchResult;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.GetSelectedOrganisationSearchResultTests
{
    [TestFixture]
    public class WhenGetSelectedOrganisationSearchResultIsHandled
    {
        private ISessionService _sessionService;
        private Guid _requestId;
        private ReferenceDataSearchResult _result;
        private Guid _selectedOrganisationId;

        [SetUp]
        public async Task SetUp()
        {
            _requestId = Guid.NewGuid();
            _sessionService = Substitute.For<ISessionService>();
            _selectedOrganisationId = Guid.NewGuid();
            _sessionService.Get<List<ReferenceDataSearchResult>>($"Searchresults-{_requestId}").Returns(new List<ReferenceDataSearchResult>()
            {
                new ReferenceDataSearchResult(){Id = Guid.NewGuid(), Name = "Org1"},
                new ReferenceDataSearchResult(){Id = _selectedOrganisationId, Name = "Org2"},
                new ReferenceDataSearchResult(){Id = Guid.NewGuid(), Name = "Org3"}
            });
            
            var handler = new GetSelectedOrganisationSearchResultHandler(_sessionService);

            _result = await handler.Handle(new GetSelectedOrganisationSearchResultRequest(_selectedOrganisationId, _requestId), CancellationToken.None);
        }
        
        [Test]
        public void ThenTheCacheIsQueriedForTheSavedSearchResults()
        {
            _sessionService.Received().Get<List<ReferenceDataSearchResult>>($"Searchresults-{_requestId}");
        }

        [Test]
        public void ThenTheSelectedOrganisationIsReturned()
        {
            _result.Id.Should().Be(_selectedOrganisationId);
            _result.Name.Should().Be("Org2");
        }
    }
}