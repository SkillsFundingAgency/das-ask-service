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

namespace SFA.DAS.ASK.Application.UnitTests.Controllers.OrganisationResultsTests
{
    [TestFixture]
    public class WhenNavigatingToTheSearchResultsPage
    {
        private const string RETURNED_SEARCH_TERM = "searchterm";
        private OrganisationResultsController _sut;

        private Mock<IMediator> _mockMediator;
        private Mock<ISessionService> _mockSessionService;

        private Guid requestId = Guid.Parse("63be476e-0593-40c5-9b8d-8f0358a4d195");

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _mockSessionService = new Mock<ISessionService>();

            _sut = new OrganisationResultsController(_mockMediator.Object, _mockSessionService.Object);
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenSearchTermIsRetrievedFromSessionStorageAsync()
        {
            _mockSessionService.Setup(s => s.Get($"Searchterm-{requestId}")).Returns(RETURNED_SEARCH_TERM);
            _mockSessionService.Setup(s => s.Get(requestId.ToString())).Returns("");//new List<ReferenceDataSearchResult>() { })
            var searchResultsPage = await _sut.Index(requestId, string.Empty);
            
            
            searchResultsPage.Should().BeAssignableTo<IActionResult>();
            var view = searchResultsPage.Should().BeOfType<ViewResult>();

           // var viewmodel = ()
            var page = (IActionResult)searchResultsPage;


        }
    }
}
