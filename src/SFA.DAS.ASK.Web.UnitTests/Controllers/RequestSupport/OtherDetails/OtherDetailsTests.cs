using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.OtherDetails
{
    [TestFixture]
    public class OtherDetailsTests : ControllersTestBase
    {
        private OtherDetailsController sut;
        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID });

            sut = new OtherDetailsController(Mediator);
        }

        [Test]
        public async Task WhenNavigatingToTheOtherDetailsPage_ThenTheCorrectViewIsDisplayed()
        {
            var actual = await sut.Index(REQUEST_ID);

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/OtherDetails.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenSubmittingTheOtherDetailsForm_ThenTheUserIsRedirectedToTheConfirmationScreen()
        {
            var actual = await sut.Index(REQUEST_ID, new OtherDetailsViewModel());

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("ApplicationComplete", redirectResult.ControllerName);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public async Task WhenSubmittingTheOtherDetailsForm_ThenTheSubmitSupportRequestHandlerIsCalled()
        {
            var actual = await sut.Index(REQUEST_ID, new OtherDetailsViewModel());

            Mediator.Received(1).Send(Arg.Any<SubmitSupportRequest>());
        }

        [Test]
        public async Task WhenSubmittingTheFormWithAnError_ThenTheUserIsRedirectedToTheForm()
        {
            sut.ModelState.AddModelError("Accept", "You must accept conditions");

            var actual = await sut.Index(REQUEST_ID, new OtherDetailsViewModel());

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
