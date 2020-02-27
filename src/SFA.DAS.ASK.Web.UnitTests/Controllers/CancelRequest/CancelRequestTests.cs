using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.CancelRequest
{
    public class CancelRequestTests : ControllersTestBase
    {

        private CancelSupportRequestController sut;
        public  ClaimsPrincipal User;
        [SetUp]
        public void Arrange()
        {
            User = Substitute.For<ClaimsPrincipal>();

            sut = new CancelSupportRequestController(Mediator);
        }

        [Test]
        public void WhenViewingTheCancelRequestPage_ThenTheCorrectViewIsRendered()
        {
            var actual = sut.Index(REQUEST_ID, "Index", "YourDetails");

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/CancelRequest.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenSubmittingWithoutSelectingAnOption_ThenRedirectsToTheCancelRequestPage()
        {
            sut.ModelState.AddModelError("CancelRequest", "You must select an option");

            var actual = await sut.Cancel(REQUEST_ID, new CancelSupportRequestViewModel(REQUEST_ID, "TestAction", "TestController"));

            var redirectResult = actual as RedirectToActionResult;
            var returnAction = redirectResult.RouteValues["ReturnAction"];
            var returnContoller = redirectResult.RouteValues["ReturnController"];

            Assert.AreEqual("TestController", returnContoller);
            Assert.AreEqual("TestAction", returnAction);
        }
        [Test]
        public async Task WhenSubmittingWithoutSelectingAnOption_ThenCancelIsNotCalled()
        {
            sut.ModelState.AddModelError("CancelRequest", "You must select an option");

            var actual = await sut.Cancel(REQUEST_ID, new CancelSupportRequestViewModel(REQUEST_ID, "TestAction", "TestController"));

            Mediator.DidNotReceive().Send(Arg.Any<CancelSupportRequestCommand>());
        }

        [Test]
        public async Task WhenSubmittingWithoutSelectingAnOption_ThenReturnControllerAndActionIsRetained()
        {
            sut.ModelState.AddModelError("CancelRequest", "You must select an option");

            var actual = await sut.Cancel(REQUEST_ID, new CancelSupportRequestViewModel(REQUEST_ID, "TestAction", "TestController"));
            
            var redirectResult = actual as RedirectToActionResult;
            var returnAction = redirectResult.RouteValues["ReturnAction"];
            var returnController = redirectResult.RouteValues["ReturnController"];

            Assert.AreEqual("TestController", returnController);
            Assert.AreEqual("TestAction", returnAction);
        }

        [Test]
        public async Task WhenCancellingTheCancelSupportRequest_ThenYouAreRedirectedToThePageYouRequestedToCancelFrom()
        {
            var actual = await sut.Cancel(REQUEST_ID, new CancelSupportRequestViewModel(REQUEST_ID, "TestAction", "TestController") { ConfirmedCancel = false });

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("TestController", redirectResult.ControllerName);
            Assert.AreEqual("TestAction", redirectResult.ActionName);
        }

        [Test]
        public async Task WhenCancellingASupportRequest_ThenCancelCommandIsCalled()
        {
            User.FindFirst(Arg.Any<string>()).Returns(new Claim("email", "test@email.com"));

            await sut.Cancel(REQUEST_ID, new CancelSupportRequestViewModel() { ConfirmedCancel = true });

            Mediator.Received().Send(Arg.Any<CancelSupportRequestCommand>());
        }

        [Test]
        public async Task WhenCancellingTheCancelSupportRequest_ThenYouAreRedirectedToTheStartPage()
        {
            User.FindFirst(Arg.Any<string>()).Returns(new Claim("email", "test@email.com"));

            var actual = await sut.Cancel(REQUEST_ID, new CancelSupportRequestViewModel(REQUEST_ID, "TestAction", "TestController") { ConfirmedCancel = true });

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("Home", redirectResult.ControllerName);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

    }
}
