using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
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

        [HttpGet("delivery-partner/planning-meeting/schedule-planning-meeting/{meetingId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid meetingId)
        
        {
            var meeting = await _mediator.Send(new GetPlanningMeetingRequest(meetingId));

            var viewModel = new SchedulePlanningMeetingViewModel(meeting);

            return View("~/Views/DeliveryPartner/PlanningMeetings/SchedulePlanningMeeting.cshtml", viewModel);
        }
    }
}