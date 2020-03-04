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

            var vm = new ConfirmDetailsViewModel(details);

            return View("~/Views/Feedback/ConfirmDetails.cshtml", vm);
        }
    }
}