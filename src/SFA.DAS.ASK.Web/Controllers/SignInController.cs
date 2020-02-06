using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class SignInController : Controller
    {
        private readonly IMediator _mediator;

        public SignInController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("sign-in")]
        public IActionResult SignIn()
        {
            return View("~/Views/RequestSupport/SignIn.cshtml");
        }

        [HttpGet("signed-in")]
        public async Task<IActionResult> SignedIn()
        {
            var request = await _mediator.Send(new StartRequestSignedInRequest(Guid.NewGuid()));

            return RedirectToAction("SignedIn", "OtherDetails", new {requestId = request.Id});
        }
    }
}