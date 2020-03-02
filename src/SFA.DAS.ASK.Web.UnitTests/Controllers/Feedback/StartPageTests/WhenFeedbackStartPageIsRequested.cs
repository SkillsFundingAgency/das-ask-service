using System;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.Feedback;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.StartPageTests
{
    [TestFixture]
    public class WhenFeedbackStartPageIsRequested
    {
        private FeedbackStartPageController _controller;
        private IActionResult _result;
        private IMediator _mediator;
        private Guid _feedbackId;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new FeedbackStartPageController(_mediator);
            _feedbackId = Guid.NewGuid();
            _mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(new VisitFeedback() {Status = FeedbackStatus.NotStarted});
        }
        
        [Test]
        public async Task ThenViewResultIsReturned()
        {
            _result = await _controller.Index(_feedbackId);
            _result.Should().BeOfType<ViewResult>();
        }

        [Test]
        public async Task AndFeedbackIsNotComplete_ThenViewResultHasStartViewName()
        {
            _result = await _controller.Index(_feedbackId);
            _result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/Start.cshtml");
        }
        
        [Test]
        public async Task AndFeedbackIsComplete_ThenViewResultHasStartViewName()
        {
            _mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(new VisitFeedback() {Status = FeedbackStatus.Complete});
            _result = await _controller.Index(_feedbackId);
            _result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/Complete.cshtml");
        }

        [Test]
        public async Task ThenFeedbackIdIsPassedToHandler()
        {
            _result = await _controller.Index(_feedbackId);
            await _mediator.Received().Send(Arg.Is<GetVisitFeedbackRequest>(req => req.FeedbackId == _feedbackId));
        }
    }
}