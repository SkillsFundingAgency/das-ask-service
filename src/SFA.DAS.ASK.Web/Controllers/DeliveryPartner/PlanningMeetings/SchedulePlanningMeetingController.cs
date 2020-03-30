using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.StartPlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class SchedulePlanningMeetingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public SchedulePlanningMeetingController(IMediator mediator, ISessionService sessionSerivce)
        {
            _mediator = mediator;
            _sessionService = sessionSerivce;
        }

        [HttpGet("delivery-partner/planning-meeting/schedule-planning-meeting/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId, bool edit)
        {
            var request = await _mediator.Send(new GetSupportRequest(supportId));
            
            //if (request.DeliveryPartnerId != _sessionService.Get<DeliveryPartnerContact>("SignedInContact").DeliveryPartnerId)
            //{
            //    return RedirectToAction("Index", "DeliveryPartnerDashboard");
            //}

            var meeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));

            if (meeting == null)
                meeting = new PlanningMeeting();

            var viewModel = new SchedulePlanningMeetingViewModel(meeting, request, edit);

            return View("~/Views/DeliveryPartner/PlanningMeetings/SchedulePlanningMeeting.cshtml", viewModel);

        }

        [HttpPost("delivery-partner/planning-meeting/schedule-planning-meeting/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid supportId, SchedulePlanningMeetingViewModel viewModel)
        {
            var request = await _mediator.Send(new GetSupportRequest(supportId));

            //if (request.DeliveryPartnerId != _sessionService.Get<DeliveryPartnerContact>("SignedInContact").DeliveryPartnerId)
            //{
            //    return RedirectToAction("Index", "DeliveryPartnerDashboard");
            //}

            if (!ModelState.IsValid)
            { 
                return RedirectToAction("Index", "SchedulePlanningMeeting", new { supportId });
            }
         
            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));

            if (planningMeeting != null)
            {
                viewModel.UpdatePlanningMeeting(planningMeeting);

                await _mediator.Send(new UpdatePlanningMeetingCommand());
            }
            else
            {
                await _mediator.Send(new StartPlanningMeetingCommand(supportId, request.DeliveryPartnerId, viewModel.Day, viewModel.Month, viewModel.Year, viewModel.Hours, viewModel.Minutes, viewModel.Type.Value));
            }
            

            if (viewModel.Edit)
            {
                return RedirectToAction("Index", "CheckAnswers", new { supportId });
            }

            return RedirectToAction("Index", "PlanningContact", new { supportId });
        }
    }
}