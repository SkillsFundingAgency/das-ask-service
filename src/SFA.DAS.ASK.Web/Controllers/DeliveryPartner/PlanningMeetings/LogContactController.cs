using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.StartPlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class LogContactController : Controller
    {
        private IMediator _mediator { get; set; }

        public LogContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("delivery-partner/planning-meeting/log-contact/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(supportId));
            
            var viewModel = new LogContactViewModel(supportRequest);

            return View("~/Views/DeliveryPartner/PlanningMeetings/Logcontact.cshtml", viewModel);
        }

        [HttpPost("delivery-partner/planning-meeting/log-contact/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid supportId, LogContactViewModel viewModel)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(supportId));
            try
            {
                viewModel.UpdateSupportRequest(supportRequest);
            }
            catch
            {
                ModelState.AddModelError("ContactedDate", "Enter a real date");
            }
            if (viewModel.Email == false && viewModel.Telephone == false)
            {
                ModelState.AddModelError("SelectedContactMethod", "Enter a contact method");
            }
            if (!ModelState.IsValid)
            {
                return View("~/Views/DeliveryPartner/PlanningMeetings/LogContact.cshtml", viewModel);
            }

            await _mediator.Send(new SaveTempSupportRequest());

            await _mediator.Send(new StartPlanningMeetingCommand(supportId));

            if (viewModel.SchedulePlanningMeeting == true)
            {
                return RedirectToAction("Index", "SchedulePlanningMeeting", new { supportId });
            }

            return RedirectToAction();
        }
    }
}