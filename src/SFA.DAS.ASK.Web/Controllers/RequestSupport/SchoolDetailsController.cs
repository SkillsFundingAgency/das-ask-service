using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class SchoolDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public SchoolDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("school-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            var vm = new SchoolDetailsViewModel(supportRequest);

            return View("~/Views/RequestSupport/SchoolDetails.cshtml", vm);
        }

        [HttpPost("school-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId, SchoolDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/RequestSupport/SchoolDetails.cshtml", viewModel);    
            }
            
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            await _mediator.Send(new SaveSupportRequest(viewModel.ToSupportRequest(supportRequest)));

            return RedirectToAction("Index", "OtherDetails", new {requestId});
        }
    }
}