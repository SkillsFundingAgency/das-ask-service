using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeeting;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class DeliveryPartnerContactController : Controller
    {
        private readonly IMediator _mediator;

        public DeliveryPartnerContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("delivery-partner/planning-meeting/delivery-partner-contact/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId)
        {
            //This will be retrieved from the sign in service or session service
            var myId = Guid.Parse("CAC458E1-C88E-4E1D-9523-2FD1BB0F9EED");
            var myOrgId = Guid.Parse("BB2D2212-7DA2-4DD9-9208-1C6715FB6216");

            var contacts = await _mediator.Send(new GetDeliveryPartnerContactsRequest(myOrgId));
            var meeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));


            var viewModel = new DeliveryPartnerContactViewModel(myId, contacts, meeting);

            return View("~/Views/DeliveryPartner/PlanningMeetings/DeliveryPartnerContact.cshtml", viewModel);
        }

        [HttpPost("delivery-partner/planning-meeting/delivery-partner-contact/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Submit(Guid supportId, DeliveryPartnerContactViewModel viewModel)
        {
            //check model state
            if (!ModelState.IsValid)
            {
                return View("~/Views.DeliveryPartner/DeliveryPArtnerContact.cshtml", viewModel);
            }

            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));

            viewModel.UpdatePlanningMeeting(planningMeeting);

            await _mediator.Send(new UpdatePlanningMeetingCommand());

            return RedirectToAction("Index", "CheckAnswers", new { supportId });
        }
    }
}