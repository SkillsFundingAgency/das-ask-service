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
        public async Task<IActionResult> Index(Guid supportId, bool edit)
        
        {
            var meeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));
            var request = await _mediator.Send(new GetSupportRequest(supportId));

            var viewModel = new SchedulePlanningMeetingViewModel(meeting, request, edit);

            return View("~/Views/DeliveryPartner/PlanningMeetings/SchedulePlanningMeeting.cshtml", viewModel);
        }

        [HttpPost("delivery-partner/planning-meeting/schedule-planning-meeting/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid supportId, SchedulePlanningMeetingViewModel viewModel)
        {
            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));

            try
            {
                viewModel.UpdatePlanningMeeting(planningMeeting);
            }
            catch 
            {
                ModelState.AddModelError("DateOfMeeting", "Enter a real date");
                return View("~/Views/DeliveryPartner/PlanningMeetings/SchedulePlanningMeeting.cshtml", viewModel);
            };

            if (planningMeeting.MeetingTimeAndDate < DateTime.Now)
            {
                ModelState.AddModelError("DateOfMeeting", "Date of planning meeting must be in the future");
                return View("~/Views/DeliveryPartner/PlanningMeetings/SchedulePlanningMeeting.cshtml", viewModel);
            }

            await _mediator.Send(new UpdatePlanningMeetingCommand());

            if (viewModel.Edit)
            {
                return RedirectToAction("Index", "CheckAnswers", new { supportId });
            }

            return RedirectToAction("Index", "PlanningContact", new { supportId });
        }
    }
}