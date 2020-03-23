using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContact;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisationContact;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class CheckAnswersController : Controller
    {
        private readonly IMediator _mediator;

        public CheckAnswersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("delivery-partner/planning-meeting/check-answers/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId)
        {
            //var myId = Guid.Parse("E9FDE3ED-8228-4A9F-9BF4-1516C06F1BD7");
            //var myOrgId = Guid.Parse("BB2D2212-7DA2-4DD9-9208-1C6715FB6216");

            var supportRequest = await _mediator.Send(new GetSupportRequest(supportId));
            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));
            var contacts = await _mediator.Send(new GetOrganisationContactsRequest(supportRequest.OrganisationId));
            var contact = contacts.Where(c => c.Id == supportRequest.OrganisationContactId).FirstOrDefault();
            var deliveryPartnerContact = await _mediator.Send(new GetDeliveryPartnerContactRequest(planningMeeting.DeliveryPartnerContactId.GetValueOrDefault()));

            var vm = new CheckAnswersViewModel(supportRequest, planningMeeting, contact, deliveryPartnerContact) ;

            return View("~/Views/DeliveryPartner/PlanningMeetings/CheckAnswers.cshtml", vm);
        }
    }
}