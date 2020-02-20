using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class YourDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public YourDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("your-details/{requestId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            var vm = new YourDetailsViewModel(supportRequest);
            
            return View("~/Views/RequestSupport/YourDetails.cshtml", vm);
        }

        [HttpPost("your-details/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, YourDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new {requestId});
            }
            
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            await _mediator.Send(new SaveTempSupportRequest(viewModel.ToTempSupportRequest(supportRequest)));
            
            return RedirectToAction("Index", "OrganisationType", new {requestId = requestId});
        }
    }
}