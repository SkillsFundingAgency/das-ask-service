using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Application.Handlers.Feedback.SaveVisitFeedback;
using SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitComplete;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.Feedback;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.YourCommentsTests
{
    [TestFixture]
    public class WhenYourCommentsAreSubmitted : FeedbackTestBase
    {
        
        private FeedbackYourCommentsController _controller;

        [SetUp]
        public void Arrange()
        {
            _controller = new FeedbackYourCommentsController(Mediator);
        }

        [Test]
        public async Task ThenUserIsRedirectedToTheFeedbackCompletePage()
        {
            Mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(GetVisitFeedback());

            var result = await _controller.Index(FEEDBACK_ID, new YourCommentsViewModel());
            
            result.As<RedirectToActionResult>().ControllerName.Should().Be("FeedbackComplete");
        }

        [Test]
        public async Task ThenTheSaveVisitFeedbackCommandIsCalledOnce()
        {
            Mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(GetVisitFeedback());

            var result = await _controller.Index(FEEDBACK_ID, new YourCommentsViewModel());

            Mediator.Received(1).Send(Arg.Any<SaveVisitFeedbackRequest>());
        }
        
        [Test]
        public async Task ThenTheSetVisitFeedbackCompleteCommandIsCalledOnce()
        {
            Mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(GetVisitFeedback());

            var result = await _controller.Index(FEEDBACK_ID, new YourCommentsViewModel());

            Mediator.Received(1).Send(Arg.Any<SetVisitFeedbackCompleteCommand>());
            
        }
    }
}
