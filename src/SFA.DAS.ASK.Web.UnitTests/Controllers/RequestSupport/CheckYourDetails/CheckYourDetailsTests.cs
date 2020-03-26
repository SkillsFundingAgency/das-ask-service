using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.CheckYourDetails
{
    public class CheckYourDetailsTests : ControllersTestBase
    {
        private CheckYourDetailsController sut;

        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID });

            SessionService.Get(Arg.Any<string>()).Returns("1");
            sut = new CheckYourDetailsController(Mediator, SessionService);
        }

        [Test]
        public async Task WhenNavigatingToTheCheckYourDetailsPage_ThenTheCorrectViewIsDisplayed()
        {
            var actual = await sut.Index(REQUEST_ID);

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/CheckYourDetails.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenNavigatingToTheCheckYourDetails_ThenTempSupportDataIsRequested()
        {
            await sut.Index(REQUEST_ID);

            await Mediator.Received().Send(Arg.Any<GetTempSupportRequest>());
        }

        [Test]
        public void WhenConfirmingDetails_ThenUserIsRedirectedToMoreDetailsPage()
        {
            var actual = sut.Continue(REQUEST_ID);

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("OtherDetails", redirectResult.ControllerName);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
