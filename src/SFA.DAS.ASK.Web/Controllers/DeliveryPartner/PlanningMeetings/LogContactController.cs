using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.Shared.UpdateSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class LogContactController : Controller
    {
        private IMediator _mediator { get; set; }
        private ISessionService _sessionService { get; set; }

        public LogContactController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
        }

        [HttpGet("delivery-partner/planning-meeting/log-contact/{supportId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid supportId)
        
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(supportId));
            
            if (supportRequest.CurrentStatus == Status.NewRequest)
            {
                var viewModel = new LogContactViewModel(supportRequest);

                return View("~/Views/DeliveryPartner/PlanningMeetings/Logcontact.cshtml", viewModel);
            }

            return RedirectToAction("Index", "DeliveryPartnerDashboard");
        }

        [HttpPost("delivery-partner/planning-meeting/log-contact/{supportId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid supportId, LogContactViewModel viewModel)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(supportId));
            DateTime date;
            bool dateParsed = DateTime.TryParse($"{viewModel.Day}/{viewModel.Month}/{viewModel.Year}", out date);
            
            if (!dateParsed)
            { 
                ModelState.AddModelError("ContactedDate", "Enter a real date");
            }
           
            if (!ModelState.IsValid)
            {
                return View("~/Views/DeliveryPartner/PlanningMeetings/LogContact.cshtml", viewModel);
            }
            
            viewModel.UpdateSupportRequest(supportRequest);

            await _mediator.Send(new UpdateSupportRequestCommand());

            if (viewModel.SchedulePlanningMeeting == true)
            {
                return RedirectToAction("Index", "SchedulePlanningMeeting", new { supportId });
            }

            return RedirectToAction();
        }
    }
}