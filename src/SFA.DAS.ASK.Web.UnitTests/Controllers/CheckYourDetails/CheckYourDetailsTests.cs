﻿using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.CheckYourDetails
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
        public async Task WhenNavigatingToTheCheckYourDetails_ThenDataIsLoaded()
        {

        }

        [Test]
        public async Task WhenNavigatingToTheCheckYourDetails_ThenTempSupportDataIsRequested()
        {
            var actual = await sut.Index(REQUEST_ID);

            Mediator.Received().Send(Arg.Any<GetTempSupportRequest>());
        }

        [Test]
        public async Task WhenConfirmingDetails_ThenUserIsRedirectedToMoreDetailsPage()
        {
            var actual = sut.Continue(REQUEST_ID);

            var redirectResult = actual as RedirectToActionResult;

            Assert.AreEqual("OtherDetails", redirectResult.ControllerName);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
