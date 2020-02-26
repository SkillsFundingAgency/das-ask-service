using MediatR;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers
{
    
    public class WhenRenderingTheYourDetailsController
    {
        protected IMediator Mediator;

        private YourDetailsController sut;
        [SetUp]
        public void Arrange()
        {
            
            Mediator = Substitute.For<IMediator>();
            
            Mediator.Send(new GetTempSupportRequest(Arg.Any<Guid>())).Returns();

            sut = new YourDetailsController(Mediator);
        }

        [Test]
        Then

    }
}
