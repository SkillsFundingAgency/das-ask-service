using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure;
using SFA.DAS.ASK.Web.Infrastructure.Filters;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class OtherDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public OtherDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("other-details/{requestId}")]
        [ServiceFilter(typeof(CheckRequestFilter))]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            var vm = new OtherDetailsViewModel(supportRequest);
            
            return View("~/Views/RequestSupport/OtherDetails.cshtml", vm);
        }
        
        [HttpPost("other-details/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, OtherDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", requestId);
            }

            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            await _mediator.Send(new SubmitSupportRequest(viewModel.ToTempSupportRequest(supportRequest)));
            
            return RedirectToAction("Index", "ApplicationComplete", new{requestId});
        }


    }
}