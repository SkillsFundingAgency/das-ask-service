using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInUserInformation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class SignInController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SignInController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
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
            //var dfeSignInId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var email = "davegouge@myschool.org.uk.edu.com";
            var dfeSignInId = Guid.NewGuid();
            string name = "David Gouge";

            var startRequestResponse = await _mediator.Send(new StartTempSupportRequestCommand(SupportRequestType.DfeSignIn));
            
            var dfeOrganisationsCheckResponse = await _mediator.Send(new DfeOrganisationsCheckRequest(dfeSignInId));
            switch (dfeOrganisationsCheckResponse.DfeOrganisationsStatus)
            {
                case DfeOrganisationsStatus.Multiple:
                    return RedirectToAction("Index", "SelectOrganisation", new {requestId = startRequestResponse.RequestId});
                    break;
                case DfeOrganisationsStatus.None:
                    await _mediator.Send(new AddDfeSignInUserInformationCommand(email, name, startRequestResponse.RequestId));
                    return RedirectToAction("Index", "OrganisationSearch", new {requestId = startRequestResponse.RequestId});
                    
                    break;
                case DfeOrganisationsStatus.Single:
                    await _mediator.Send(new AddDfESignInInformationCommand(dfeSignInId, dfeOrganisationsCheckResponse.Urn, email, name, startRequestResponse.RequestId));
            
                    return RedirectToAction("SignedIn", "OtherDetails", new {requestId = startRequestResponse.RequestId});
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}