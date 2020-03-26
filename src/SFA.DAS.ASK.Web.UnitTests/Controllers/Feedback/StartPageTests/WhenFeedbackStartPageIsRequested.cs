using System;
using System.Security;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
            _result.Should().BeOfType<RedirectToActionResult>();
        }

        [Test]
        public async Task ThenFeedbackIdIsPassedToHandler()
        {
            _result = await _controller.Index(_feedbackId);
            await _mediator.Received().Send(Arg.Is<GetVisitFeedbackRequest>(req => req.FeedbackId == _feedbackId));
        }
        
        [Test]
        public async Task AndFeedbackIsNotComplete_ThenResultIdRedirectToConfirmDetailsPage()
        {
            _result = await _controller.Index(_feedbackId);

            _result.Should().BeOfType<RedirectToActionResult>();
            _result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            _result.As<RedirectToActionResult>().ControllerName.Should().Be("FeedbackConfirmDetails");
        }
        
        [Test]
        public async Task AndFeedbackIsComplete_ThenResultIsRedirectToCompletePage()
        {
            _mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(new VisitFeedback() {Status = FeedbackStatus.Complete});
            _result = await _controller.Index(_feedbackId);

            _result.Should().BeOfType<RedirectToActionResult>();
            _result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            _result.As<RedirectToActionResult>().ControllerName.Should().Be("FeedbackComplete");
        }
        
        [Test]
        public void AndFeedbackIdIsNotValid_ThenSecurityExceptionThrown()
        {
            _mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(default(VisitFeedback));

            _controller.Invoking(c => c.Index(_feedbackId)).Should().Throw<SecurityException>().WithMessage($"Feedback ID {_feedbackId} is not valid.");
        }
    }
}