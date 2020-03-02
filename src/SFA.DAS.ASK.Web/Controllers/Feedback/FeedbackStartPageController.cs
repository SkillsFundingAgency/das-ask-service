using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
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
            return View(visitFeedback.Status == FeedbackStatus.Complete ? "~/Views/Feedback/Complete.cshtml" : "~/Views/Feedback/Start.cshtml");
        }
    }
}