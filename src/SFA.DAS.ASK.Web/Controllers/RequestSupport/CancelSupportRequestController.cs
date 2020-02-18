using System;
using System.Threading.Tasks;
using MediatR;
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
    }
}