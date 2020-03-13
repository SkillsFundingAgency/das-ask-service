using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using FluentAssertions;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.HasSignIn
{
    [TestFixture]
    public class HasSignInTests : ControllersTestBase
    {
        private HasSignInController sut;
        [SetUp]
        public void Arrange()
        {

            sut = new HasSignInController(Mediator, SessionService);
        }

        [Test]
        [Ignore("Ignoring as the has sign in page is bypassed for the time being")]
        public async Task WhenNavigatingToTheHasSignInPage_ThenTheCorrectViewIsDisplayed()
        {
            SessionService.Get(Arg.Is("HasSignIn")).Returns("true");

            var actual = await sut.Index();

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/HasSignIn.cshtml", viewResult.ViewName);
        }
        
        [Test]
        public async Task WhenNavigatingToTheHasSignInPage_ThenTheARedirectToYourDetailsIsReturned()
        {
            SessionService.Get(Arg.Is("TempSupportRequestId")).Returns(REQUEST_ID.ToString());

            var actual = await sut.Index();

            actual.Should().BeOfType<RedirectToActionResult>();
        }

        [Test]
        public async Task WhenSubmittingYes_ThenUserRedirectedToSignInPage()
        {
            var actual = await sut.Index(new HasSignInViewModel() { HasSignInAccount = true });

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("SignIn", redirectResult.ActionName);
            Assert.AreEqual("RequestSupportSignIn", redirectResult.ControllerName);
        }

        [Test]
        public async Task WhenNoOptionsAreSelected_ThenUserIsRedirectedToTheHasSignInScreen()
        {
            sut.ModelState.AddModelError("HasSignIn", "Select an option");

            var actual = await sut.Index(new HasSignInViewModel());

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public async Task WhenTheNoDfeSignInOptionIsSelected_ThenUserIsRedirectedToYourDetailsPage()
        {
            SessionService.Get(Arg.Is("TempSupportRequestId")).Returns(REQUEST_ID.ToString());

            var actual = await sut.Index(new HasSignInViewModel() { HasSignInAccount = false });

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("YourDetails", redirectResult.ControllerName);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public async Task WhenTheNoDfeSignInOptionIsSelected_ThenNewSupportRequestCommandIsCalled()
        {
            SessionService.Get(Arg.Is("TempSupportRequestId")).ReturnsNull();
            Mediator.Send(Arg.Any<StartTempSupportRequestCommand>()).Returns(new StartTempSupportRequestResponse(REQUEST_ID));

            var actual = await sut.Index(new HasSignInViewModel() { HasSignInAccount = false });

            Mediator.Received(1).Send(Arg.Any<StartTempSupportRequestCommand>());
        }
    }
}
