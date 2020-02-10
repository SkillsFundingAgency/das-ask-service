using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
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
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            var vm = new YourDetailsViewModel(supportRequest);
            
            return View("~/Views/RequestSupport/YourDetails.cshtml", vm);
        }

        [HttpPost("your-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId, YourDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/RequestSupport/YourDetails.cshtml", viewModel);
            }
            
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            await _mediator.Send(new SaveSupportRequest(viewModel.ToSupportRequest(supportRequest)));
            
            return RedirectToAction("Index", "OrganisationDetails", new {requestId = requestId});
        }
    }
}