using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.Home
{
    [TestFixture]
    public class HomeControllerTests : ControllersTestBase
    {
        private HomeController sut;
        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = FIRST_NAME, Id = REQUEST_ID });

            sut = new HomeController();
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
