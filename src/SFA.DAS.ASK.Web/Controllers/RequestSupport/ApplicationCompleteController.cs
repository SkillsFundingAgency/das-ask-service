using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class ApplicationCompleteController : Controller
    {
        private readonly IMediator _mediator;

        public ApplicationCompleteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("application-complete/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            var vm = new ApplicationCompleteViewModel(){Email = supportRequest.Email};
            return View("~/Views/RequestSupport/ApplicationComplete.cshtml", vm);
        }
    }
}