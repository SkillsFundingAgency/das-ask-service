using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.OrganisationType
{
    [TestFixture]
    public class OrganisationTypeTests : ControllersTestBase
    {
        private OrganisationTypeController sut;

        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID });

            sut = new OrganisationTypeController(Mediator);
        }

        [Test]
        public async Task WhenVisitingTheOrganisationTypePage_ThenTheCorrectViewIsDisplayed()
        {
            var actual = await sut.Index(REQUEST_ID);

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/OrganisationType.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenSubmitingTheFormWithAnError_ThenTheViewIsReloaded()
        {
            sut.ModelState.AddModelError("OrganisationType", "You must select an organisation type");

            var actual = await sut.Index(REQUEST_ID, new OrganisationTypeViewModel());

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/OrganisationType.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenSubmittingTheFormWithoutAnAnswerForOther_ThenTheViewIsReloaded()
        {
            var actual = await sut.Index(REQUEST_ID, new OrganisationTypeViewModel() { SelectedOrganisationType = Data.Entities.OrganisationType.Other });

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/OrganisationType.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenSubmittingAValidForm_ThenSaveTempSupportRequestIsCalled()
        {
            await sut.Index(REQUEST_ID, new OrganisationTypeViewModel() { SelectedOrganisationType = Data.Entities.OrganisationType.School });

            await Mediator.Received(1).Send(Arg.Any<SaveTempSupportRequest>());
        }

        [Test]
        public async Task WhenSubmittingAValidForm_ThenUserIsRedirectedToOrganisationSearchPage()
        {
            var actual = await sut.Index(REQUEST_ID, new OrganisationTypeViewModel() { SelectedOrganisationType = Data.Entities.OrganisationType.School });

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("OrganisationSearch", redirectResult.ControllerName);
        }
    }
}
