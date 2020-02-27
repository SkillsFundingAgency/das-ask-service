using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers
{
    
    public class YourDetailsControllerTests : ControllersTestBase
    {
        
        private YourDetailsController sut;

        [SetUp]
        public void Arrange()
        {
            
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID });

            sut = new YourDetailsController(Mediator);
        }

        [Test]
        public async Task WhenViewingTheYourDetailsPage_ThenTheCorrectViewIsRendered()
        {

            var actual = await sut.Index(REQUEST_ID, false);

            var viewResult = actual as ViewResult;
            var viewModel = viewResult.ViewData.Model as YourDetailsViewModel;
            
            Assert.AreEqual(FIRST_NAME, viewModel.FirstName);
            Assert.AreEqual("~/Views/RequestSupport/YourDetails.cshtml", viewResult.ViewName);
        }

        [Test]
        public async Task WhenViewingTheYourDetailsPage_ThenGetTempSupportRequestIsSent()
        {
            await sut.Index(REQUEST_ID, false);

            Mediator.Received().Send(Arg.Any<GetTempSupportRequest>());
        }

        [Test]
        public async Task WhenViewingTheYourDetailsPage_ThenTheModelIsPrePopulated()
        {

            var actual = await sut.Index(REQUEST_ID, false);

            var viewResult = actual as ViewResult;
            var viewModel = viewResult.ViewData.Model as YourDetailsViewModel;

            Assert.AreEqual(FIRST_NAME, viewModel.FirstName);
        }

        [Test]
        public async Task WhenSavingYourDetails_ThenRedirectsBackToYourDetailsPageIfTheModelStateIsInvalid()
        {
            sut.ModelState.AddModelError("Name", "Enter your name");

            var actual = await sut.Index(REQUEST_ID, new YourDetailsViewModel());

            var viewResult = actual as RedirectToActionResult;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual(viewResult.ControllerName, "YourDetails");
        }

        [Test]
        public async Task WhenSavingYourDetails_ThenRedirectsToCheckYourDetailsPageIfEditing()
        {
            var actual = await sut.Index(REQUEST_ID, new YourDetailsViewModel(new TempSupportRequest(), Guid.NewGuid(), true));

            var viewResult = actual as RedirectToActionResult;

            Assert.AreEqual("CheckYourDetails", viewResult.ControllerName);
        }

        [Test]
        public async Task WhenSavingYourDetails_ThenRedirectsToOrganisationTypeIfModelIsValid()
        {
            var actual = await sut.Index(REQUEST_ID, new YourDetailsViewModel());

            var viewResult = actual as RedirectToActionResult;

            Assert.AreEqual("OrganisationType", viewResult.ControllerName);
        }
    }
}
