using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInUserInformation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class RequestSupportSignInController : Controller
    {
        private readonly IMediator _mediator;

        public RequestSupportSignInController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task SignIn()
        {
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties() { RedirectUri = Url.Action("SignedIn", "RequestSupportSignIn") });
        }

        public async Task<IActionResult> SignedIn()
        {
            var email = User.FindFirst("email").Value;
            var dfeSignInId = Guid.Parse(User.FindFirst("sub").Value);
            var firstname = User.FindFirst("given_name").Value;
            var lastname = User.FindFirst("family_name").Value;
            // var email = "davegouge@myschool.org.uk.edu.com";
            // var dfeSignInId = Guid.NewGuid();
            // string name = "David Gouge";

            var startRequestResponse = await _mediator.Send(new StartTempSupportRequestCommand(SupportRequestType.DfeSignIn));
            
            var dfeOrganisationsCheckResponse = await _mediator.Send(new DfeOrganisationsCheckRequest(dfeSignInId));
            switch (dfeOrganisationsCheckResponse.DfeOrganisationsStatus)
            {
                case DfeOrganisationsStatus.Multiple:
                    return RedirectToAction("Index", "SelectOrganisation", new {requestId = startRequestResponse.RequestId});
                    break;
                case DfeOrganisationsStatus.None:
                    await _mediator.Send(new AddDfeSignInUserInformationCommand(email, firstname, lastname, startRequestResponse.RequestId));
                    return RedirectToAction("Index", "OrganisationSearch", new {requestId = startRequestResponse.RequestId});
                    
                    break;
                case DfeOrganisationsStatus.Single:
                    await _mediator.Send(new AddDfESignInInformationCommand(dfeSignInId, dfeOrganisationsCheckResponse.Id, email, firstname, lastname, startRequestResponse.RequestId));
            
                    return RedirectToAction("SignedIn", "OtherDetails", new {requestId = startRequestResponse.RequestId});
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}