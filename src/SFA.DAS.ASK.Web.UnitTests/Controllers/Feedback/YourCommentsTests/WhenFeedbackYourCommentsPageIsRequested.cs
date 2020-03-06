using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Controllers.Feedback;
using MediatR;
using NSubstitute;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Data.Entities;
using FluentAssertions;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.YourCommentsTests
{
    [TestFixture]
    public class WhenFeedbackYourCommentsPageIsRequested : FeedbackTestBase
    {
        private FeedbackYourCommentsController controller;
        private IMediator _mediator;
        [SetUp]
        public void Arrange()
        {
            _mediator = Substitute.For<IMediator>();

            controller = new FeedbackYourCommentsController(_mediator); 
        }

        [Test]
        public async Task ThenTheCorrectViewIsDisplayed()
        {
            _mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(GetVisitFeedback());
   
            var result = await controller.Index(FEEDBACK_ID);

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/YourComments.cshtml");
        }
    }
}
