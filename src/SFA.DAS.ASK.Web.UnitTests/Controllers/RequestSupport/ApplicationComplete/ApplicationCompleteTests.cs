using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.ApplicationComplete
{
    [TestFixture]
    public class ApplicationCompleteTests : ControllersTestBase
    {
        private ApplicationCompleteController sut;

        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID, Email = "test@email.co.uk" });

            sut = new ApplicationCompleteController(Mediator);
        }

        [Test]
        public async Task WhenVisitingTheApplicationCompletePage_ThenTheCorrectViewIsDisplayed()
        {
            var actual = await sut.Index(REQUEST_ID);

            var viewResult = actual as ViewResult;

            Assert.AreEqual("~/Views/RequestSupport/ApplicationComplete.cshtml", viewResult.ViewName);
        }
    }
}
