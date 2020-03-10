using System;
using System.Security;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Application.Handlers.Feedback.StartFeedback;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    public class FeedbackStartPageController : Controller
    {
        private readonly IMediator _mediator;

        public FeedbackStartPageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("feedback/start/{feedbackId}")]
        public async Task<IActionResult> Index(Guid feedbackId)
        {
            var visitFeedback = await _mediator.Send(new GetVisitFeedbackRequest(feedbackId, true));
            if (visitFeedback is null)
            {
                throw new SecurityException($"Feedback ID {feedbackId} is not valid.");
            }

            if (visitFeedback.Status == FeedbackStatus.Complete)
            {
                return RedirectToAction("Index", "FeedbackComplete", new {feedbackId});
            }

            return await StartFeedback(feedbackId);
        }

        [HttpPost("feedback/start/{feedbackId}")]
        public async Task<IActionResult> StartFeedback(Guid feedbackId)
        {
            await _mediator.Send(new StartFeedbackCommand(feedbackId));

            return RedirectToAction("Index", "FeedbackConfirmDetails", new {feedbackId});
        }
    }
}