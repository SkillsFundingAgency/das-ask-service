using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class DeliveryPartnerContactController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public DeliveryPartnerContactController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
        }

        [HttpGet("delivery-partner/planning-meeting/delivery-partner-contact/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId, bool edit)
        {
            //This will be retrieved from the sign in service or session service
            //var myId = Guid.Parse("CAC458E1-C88E-4E1D-9523-2FD1BB0F9EED");
            //var myOrgId = Guid.Parse("BB2D2212-7DA2-4DD9-9208-1C6715FB6216");
           
            //var signedInUser = _sessionService.Get<DeliveryPartnerContact>("SignedInContact");

            var supportRequest = await _mediator.Send(new GetSupportRequest(supportId));

            //if (supportRequest.DeliveryPartnerId != myOrgId)
            //{
            //    return RedirectToAction("Index", "DeliveryPartnerDashboard");
            //}

            var contacts = await _mediator.Send(new GetDeliveryPartnerContactsRequest(supportRequest.DeliveryPartnerId));
            var meeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));
           
            var viewModel = new DeliveryPartnerContactViewModel(contacts, meeting, supportRequest.DeliveryPartnerId, edit);

            return View("~/Views/DeliveryPartner/PlanningMeetings/DeliveryPartnerContact.cshtml", viewModel);
        }

        [HttpPost("delivery-partner/planning-meeting/delivery-partner-contact/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Submit(Guid supportId, DeliveryPartnerContactViewModel viewModel)
        {
            if (viewModel.SelectedDeliveryPartnerContactId == Guid.Empty)
            {
                ModelState.AddModelError("SelectedDeliveryPartnerContactId", "Select an option");
              
                return RedirectToAction("Index", "DeliveryPartnerContact", new { supportId });
            }

            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));

            viewModel.UpdatePlanningMeeting(planningMeeting);

            await _mediator.Send(new UpdatePlanningMeetingCommand());

            if (viewModel.Edit)
            {
                return RedirectToAction("Index", "CheckAnswers", new { supportId });
            }

            return RedirectToAction("Index", "CheckAnswers", new { supportId });
        }
    }
}