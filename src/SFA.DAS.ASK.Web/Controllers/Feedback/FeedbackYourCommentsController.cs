using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Web.ViewModels.Feedback;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    public class FeedbackYourCommentsController : Controller
    {
        private IMediator _mediator;

        public FeedbackYourCommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/feedback/your-comments/{feedbackId}")]
        public async Task<IActionResult> Index(Guid feedbackId)
        {
            var feedback = await _mediator.Send(new GetVisitFeedbackRequest(feedbackId, true));

            var viewModel = new YourCommentsViewModel(feedback);

            return View("~/Views/Feedback/YourComments.cshtml", viewModel);
        }

        [HttpPost("feedback/your-comments/{feedbackId}")]
        public async Task<IActionResult> SubmitFeedback(YourCommentsViewModel yourComments)
        {
            return RedirectToAction("Index", "FeedbackComplete", new { feedbackId = yourComments.FeedbackId });
        }
    }
}