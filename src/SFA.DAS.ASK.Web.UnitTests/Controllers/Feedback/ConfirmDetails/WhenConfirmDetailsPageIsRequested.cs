using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.Feedback;
using System.Security;
using System.Threading.Tasks;

using ViewResult = Microsoft.AspNetCore.Mvc.ViewResult;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.ConfirmDetails
{
    [TestFixture]
    public class WhenConfirmDetailsPageIsRequested : FeedbackTestBase
    {
        private FeedbackConfirmDetailsController _controller;
        

        [SetUp]
        public void Arrange()
        {
            _controller = new FeedbackConfirmDetailsController(Mediator);
        }

        [Test]
        public async Task ThenAViewResultIsReturned()
        {
            Mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(GetVisitFeedback());

            var result = await _controller.Index(FeedbackId);

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/ConfirmDetails.cshtml");
        }

        [Test]
        public async Task AndFeedbackIsCompleted_ThenRedirectedToCompletePage()
        {
            Mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(new VisitFeedback() { Status = FeedbackStatus.Complete });
            var result = await _controller.Index(FeedbackId);

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("FeedbackComplete");

        }

        [Test]
        public void AndFeedbackIdIsNotValid_ThenSecurityExceptionThrown()
        {
            Mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(default(VisitFeedback));

            _controller.Invoking(c => c.Index(FeedbackId)).Should().Throw<SecurityException>().WithMessage($"Feedback ID {FeedbackId} is not valid.");
        }
    }
}
