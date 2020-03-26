﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.OrganisationSearch
{
    public class OrganisationSearchTests : ControllersTestBase
    {

        private OrganisationSearchController sut;

        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID });
            
            sut = new OrganisationSearchController(Mediator);
        }

        [Test]
        public async Task WhenViewingTheOrganisationSearchPage_ThenTheCorrectViewIsDisplayed()
        {
            var actual = await sut.Index(REQUEST_ID, null);

            var viewResult = actual as ViewResult;
            Assert.AreEqual("~/Views/RequestSupport/OrganisationSearch.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenViewingTheOrganisationSearchPage_ThenGetTempSupportRequestIsSent()
        {
            await sut.Index(REQUEST_ID, null);

            await Mediator.Received().Send(Arg.Any<GetTempSupportRequest>());
        }

        [Test]
        public void WhenSubmittingASearchRequest_ThenRedirectsToTheSearchResultsPage()
        {
            var actual = sut.Search(REQUEST_ID, new OrganisationSearchViewModel() { Search = "Test School" });

            var actionResult = actual as RedirectToActionResult;
            
            actionResult.RouteValues.ContainsKey("Search");
            
            object searchValue;
            var search = actionResult.RouteValues.TryGetValue("Search", out searchValue);

            Assert.AreEqual(actionResult.ControllerName, "OrganisationResults");
            Assert.AreEqual(actionResult.ActionName, "Index");
            Assert.IsTrue(search);
            Assert.AreEqual(searchValue, "Test School");
        }

        [Test]
        public void WhenSubmittingAnEmptySearchRequest_ThenRedirectsToTheSearchPage()
        {
            sut.ModelState.AddModelError("Search", "Enter a search term");

            var actual = sut.Search(REQUEST_ID, new OrganisationSearchViewModel());

            var viewResult = actual as RedirectToActionResult;

            Assert.AreEqual(viewResult.ControllerName, "OrganisationSearch");
            Assert.AreEqual(viewResult.ActionName, "Index");
        }
    }
}
