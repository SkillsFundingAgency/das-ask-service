using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Home
{
    [TestFixture]
    public class HomeControllerTests : ControllersTestBase
    {
        private HomeController sut;
        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID });

            sut = new HomeController(Mediator, SessionService);
        }

        [Test]
        public async Task WhenVisitingTheStartPage_ThenTheCorrectViewIsDisplayed()
        {
            var actual = sut.Index();

            var viewResult = actual as ViewResult;

            Assert.IsNotNull(viewResult);
        }
    }
}
