using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class SignInController : Controller
    {
        private readonly IMediator _mediator;

        public SignInController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("sign-in")]
        // [Authorize]
        public IActionResult SignIn()
        {
            return View("~/Views/RequestSupport/SignIn.cshtml");
        }

        [HttpGet("signed-in")]
        public async Task<IActionResult> SignedIn()
        {
            //var email = User.FindFirst(ClaimTypes.Email);
            var email = "davegouge@myschool.org.uk.edu.com";
            //var dfeSignInId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dfeSignInId = Guid.NewGuid();
            string name = "David Gouge";
            var request = await _mediator.Send(new StartRequestSignedInRequest(dfeSignInId, email, name));

            return RedirectToAction("SignedIn", "OtherDetails", new {requestId = request.Id});
        }
    }
}