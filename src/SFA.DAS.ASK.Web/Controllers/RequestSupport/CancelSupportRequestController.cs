using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class CancelSupportRequestController : Controller
    {
        private readonly IMediator _mediator;

        public CancelSupportRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cancel-request/{requestId}")]
        public async Task<IActionResult> Cancel(Guid requestId)
        {
            var email = User.FindFirst("email").Value;
            await _mediator.Send(new CancelSupportRequestCommand(requestId, email));
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("cancel-request/{requestId}")]
        public  IActionResult Index(Guid requestId, string returnAction, string returnController)
        {
            var vm = new CancelSupportRequestViewModel(requestId, returnAction, returnController);
            
            return View("~/Views/RequestSupport/CancelRequest.cshtml", vm);
        }
    }

    public class CancelSupportRequestViewModel
    {
        public Guid RequestId { get; }
        public string ReturnAction { get; }
        public string ReturnController { get; }

        public CancelSupportRequestViewModel(Guid requestId, string returnAction, string returnController)
        {
            RequestId = requestId;
            ReturnAction = returnAction;
            ReturnController = returnController;
        }
    }
}