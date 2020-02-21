using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Moq;
using NUnit;
using NUnit.Framework;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Application.Services.Session;
using FluentAssertions;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using System.Threading;

namespace SFA.DAS.ASK.Application.UnitTests.Controllers.OrganisationResultsTests
{
    [TestFixture]
    public class WhenSearchingForAnOrganisation
    {
        private const string RETURNED_NO_ORGANISATION_RESULTS = "[]";
        private const string RETURNED_ORGANISATION_RESULTS = "[]";
        private const string SEARCH_TERM = "school";

        private OrganisationResultsController _sut;

        public Mock<IMediator> _mockMediator;
        private Mock<ISessionService> _mockSessionService;

        private Guid requestId = Guid.Parse("63be476e-0593-40c5-9b8d-8f0358a4d195");



        [SetUp]
        public async Task Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _mockSessionService = new Mock<ISessionService>();


            _sut = new OrganisationResultsController(_mockMediator.Object, _mockSessionService.Object);
        }

        [Test]
        public async Task ThenASearchTermIsSavedToCache()
        {
            _mockSessionService.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<string>()));

            var searchResultsPage = await _sut.Index(requestId, SEARCH_TERM);

            _mockSessionService.Verify(s => s.Set($"Searchstring-{requestId}", It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task ThenTheSearchResultsAreSavedToCache()
        {
            _mockSessionService.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<string>()));
            
            var searchResultsPage = await _sut.Index(requestId, SEARCH_TERM);

            _mockSessionService.Verify(s => s.Set($"Searchresults-{requestId}", It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task ThenNewGuidsAreMappedToOrganisationResults()
        {
            _mockMediator.Setup(s => s.Send(It.IsAny<GetNonDfeOrganisationsRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(GetResultsList());

            var results = await _sut.Index(requestId, SEARCH_TERM) as ViewResult;

            var model = results.Model as OrganisationResultsViewModel;

            Assert.IsNotNull(model.Results[0].Id);

            _mockMediator.Verify(s => s.Send(It.IsAny<GetNonDfeOrganisationsRequest>(), default(CancellationToken)), Times.Once);
        }

        private IEnumerable<ReferenceDataSearchResult> GetResultsList()
        {
            var results = new List<ReferenceDataSearchResult> { new ReferenceDataSearchResult { Name = "Test Schools 1" } };
          
            return results;
        }
    }
}
