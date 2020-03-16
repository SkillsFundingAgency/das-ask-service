using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeeting;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.Shared.CreateOrganisationContact;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class PlanningContactController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public PlanningContactController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
        }

        [HttpGet("delivery-partner/planning-meeting/planning-contact/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(supportId));
            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));
            var contacts = await _mediator.Send(new GetOrganisationContactsRequest(supportRequest.OrganisationId));

            _sessionService.Set<List<OrganisationContact>>($"contacts-{supportId}", contacts);

            var vm = new PlanningContactViewModel(supportRequest, planningMeeting, contacts) ;

            return View("~/Views/DeliveryPartner/PlanningMeetings/PlanningContact.cshtml", vm);
        
        }

        [HttpPost("delivery-partner/planning-meeting/planning-contact/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid supportId, PlanningContactViewModel vm)
        {
            if (vm.SelectedContact == Guid.Empty)
            {
                ValidateNewContact(vm);

                if (ModelState.ErrorCount > 1)
                {
                    vm.Contacts = _sessionService.Get<List<OrganisationContact>>($"contacts-{supportId}");

                    return View("~/Views/DeliveryPartner/PlanningMeetings/PlanningContact.cshtml", vm);
                }
                else
                {
                    var newcontact = new OrganisationContact()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = vm.NewFirstName,
                        LastName = vm.NewLastName,
                        PhoneNumber = vm.NewPhoneNumber,
                        Email = vm.NewEmail,
                        OrganisationId = vm.OrganisationId,
                        JobRole = "Not Specified"
                    };

                    await _mediator.Send(new CreateOrganisationContactCommand(newcontact));

                    return RedirectToAction("Index", "DeliveryPartnerContact", new { supportId = supportId });
                }
            }
            var planningMeeting = await _mediator.Send(new GetPlanningMeetingRequest(supportId));

            vm.UpdatePlanningMeeting(planningMeeting);

            await _mediator.Send(new UpdatePlanningMeetingCommand());

            return RedirectToAction("Index", "DeliveryPartnerContact", new { supportId = supportId });
                 
        }

        private void ValidateNewContact(PlanningContactViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.NewFirstName))
            {
                ModelState.AddModelError("NewFirstName", "Please enter a first name");
            }
            if (string.IsNullOrWhiteSpace(vm.NewLastName))
            {
                ModelState.AddModelError("NewLastName", "Please enter a last name");
            }
            if (string.IsNullOrWhiteSpace(vm.NewPhoneNumber))
            {
                ModelState.AddModelError("NewPhoneNumber", "Please enter a phone number");
            }
            if (string.IsNullOrWhiteSpace(vm.NewEmail))
            {
                ModelState.AddModelError("NewEmail", "Please enter an email");
            }
        }
    }
}