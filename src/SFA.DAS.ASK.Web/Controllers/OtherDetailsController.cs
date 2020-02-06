using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class OtherDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public OtherDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("other-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            var vm = new OtherDetailsViewModel(supportRequest){NonSignedIn = true};
            
            return View("~/Views/RequestSupport/OtherDetails.cshtml", vm);
        }

        [HttpPost("other-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId, OtherDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/RequestSupport/OtherDetails.cshtml", viewModel);
            }

            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            await _mediator.Send(new SubmitSupportRequest(viewModel.ToSupportRequest(supportRequest)));
            
            return RedirectToAction("Index", "ApplicationComplete", new{requestId});
        }

        
        [HttpGet("other-details-signed-in/{requestId}")]
        public async Task<IActionResult> SignedIn(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            var vm = new OtherDetailsViewModel(supportRequest){NonSignedIn = false};
            
            return View("~/Views/RequestSupport/OtherDetails.cshtml", vm);
        }
    }
}