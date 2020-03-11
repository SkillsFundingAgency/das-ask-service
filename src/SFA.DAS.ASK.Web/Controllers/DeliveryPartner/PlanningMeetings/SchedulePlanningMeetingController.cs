using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeeting;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class SchedulePlanningMeetingController : Controller
    {
        private readonly IMediator _mediator;

        public SchedulePlanningMeetingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("delivery-partner/planning-meeting/schedule-planning-meeting/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId)
        
        {
            var meeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));
            var request = await _mediator.Send(new GetSupportRequest(supportId));

            var viewModel = new SchedulePlanningMeetingViewModel(meeting, request);

            return View("~/Views/DeliveryPartner/PlanningMeetings/SchedulePlanningMeeting.cshtml", viewModel);
        }

        [HttpPost("delivery-partner/planning-meeting/schedule-planning-meeting/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid supportId, SchedulePlanningMeetingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "SchedulePlanningMeeting", new { supportId});
            }

            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));

            viewModel.UpdatePlanningMeeting(planningMeeting);

            await _mediator.Send(new UpdatePlanningMeetingCommand());

            return RedirectToAction("Index", "PlanningContact", new { supportId });
        }
    }
}