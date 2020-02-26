using MediatR;
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

namespace SFA.DAS.ASK.Web.UnitTests.Controllers
{
    
    public class WhenRenderingTheYourDetailsController
    {
        private IMediator Mediator;

        private YourDetailsController sut;
        private Guid requestId;

        [SetUp]
        public void Arrange()
        {
            requestId = Guid.NewGuid();

            Mediator = Substitute.For<IMediator>();

            Mediator.Send(Arg.Any<GetTempSupportRequest>()).Returns(new TempSupportRequest { FirstName = "First Name", Id = requestId });

            sut = new YourDetailsController(Mediator);
        }

        [Test]
        public async Task Then()
        {
           
            var actual = await sut.Index(requestId, false);
            
            var result = actual as ViewResult;
            
            Assert.AreEqual("Index", result.ViewName);
           // Assert.AreEqual("Test Name", result.Model.);
        }

    }
}
