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
    public class WhenNavigatingBackToTheSearchResultsPage
    {
        private const string RETURNED_SEARCH_TERM = "searchterm";
        private const string RETURNED_NO_ORGANISATION_RESULTS = "[]";
        private const string RETURNED_ORGANISATION_RESULTS = "[]";
        private const string SEARCH_TERM = "school";

        private OrganisationResultsController _sut;

        private Mock<IMediator> _mockMediator;
        private Mock<ISessionService> _mockSessionService;

        private Guid requestId = Guid.Parse("63be476e-0593-40c5-9b8d-8f0358a4d195");



        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _mockSessionService = new Mock<ISessionService>();

            _mockSessionService.Setup(s => s.Get($"Searchterm-{requestId}")).Returns(RETURNED_SEARCH_TERM);
            _mockSessionService.Setup(s => s.Get($"Searchresults-{requestId}")).Returns("");
            _mockSessionService.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<string>()));

            _sut = new OrganisationResultsController(_mockMediator.Object, _mockSessionService.Object);
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenSearchTermThenCachedResultsAreRetrievedFromSessionStorageAsync()
        {
            var searchResultsPage = await _sut.Index(requestId, string.Empty);

            _mockSessionService.Verify(x => x.Get($"Searchstring-{requestId}"), Times.Once);
            _mockSessionService.Verify(x => x.Get($"Searchresults-{requestId}"), Times.Once);
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenTheGetNonDfeOrganisationRequestIsNeverCalled()
        {
            _mockMediator.Setup(s => s.Send(It.IsAny<GetNonDfeOrganisationsRequest>(), default(CancellationToken)));

            var searchResultsPage = await _sut.Index(requestId, string.Empty);

            _mockMediator.Verify(s => s.Send(It.IsAny<GetNonDfeOrganisationsRequest>(), default(CancellationToken)), Times.Never);
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenSaveResultsToCacheIsNotCalled()
        {
            var searchResultsPage = await _sut.Index(requestId, string.Empty);

            _mockSessionService.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }
    }
}
