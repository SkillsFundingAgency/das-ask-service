using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.Feedback.AddAmmendmentComment;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.ViewModels.Feedback;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    public class FeedbackConfirmDetailsController : Controller
    {
        private IMediator _mediator;

        public FeedbackConfirmDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("feedback/confirm-details/{feedbackId}")]
        public async Task<IActionResult> Index(Guid feedbackId)
        {
            var details = await _mediator.Send(new GetVisitFeedbackRequest(feedbackId, true));

            if (details is null)
            {
                throw new SecurityException($"Feedback ID {feedbackId} is not valid.");
            }

            if (details.Status == FeedbackStatus.Complete)
            {
                return RedirectToAction("Index", "FeedbackComplete", new { feedbackId });
            }

            var vm = new ConfirmDetailsViewModel(details);

            return View("~/Views/Feedback/ConfirmDetails.cshtml", vm);
        }

        [HttpPost("feedback/confirm-details/{feedbackId}")]
        public async Task<IActionResult> StartFeedback(Guid feedbackId, ConfirmDetailsViewModel vm)
        {
            if (vm.IncorrectDetailsComments != null)
            {
                await _mediator.Send(new AddAmendmentCommentCommand(feedbackId, vm.IncorrectDetailsComments));
            }

            return RedirectToAction("Index", "FeedbackSection1", new { feedbackId = feedbackId });
        }
    }
}