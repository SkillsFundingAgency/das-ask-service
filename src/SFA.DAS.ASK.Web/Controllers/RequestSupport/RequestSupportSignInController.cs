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
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class RequestSupportSignInController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public RequestSupportSignInController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
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

            var startRequestResponse = await _mediator.Send(new StartTempSupportRequestCommand(SupportRequestType.DfeSignIn));
            
            var dfeOrganisationsCheckResponse = await _mediator.Send(new DfeOrganisationsCheckRequest(dfeSignInId));
            
            _sessionService.Set("NumberOfOrgs",((int)dfeOrganisationsCheckResponse.DfeOrganisationCheckResult).ToString());
            
            switch (dfeOrganisationsCheckResponse.DfeOrganisationCheckResult)
            {
                case DfeOrganisationCheckResult.Multiple:
                    await _mediator.Send(new AddDfeSignInUserInformationCommand(email, firstname, lastname, startRequestResponse.RequestId, dfeSignInId));
                    return RedirectToAction("Index", "SelectOrganisation", new {requestId = startRequestResponse.RequestId});
                    
                case DfeOrganisationCheckResult.None:
                    await _mediator.Send(new AddDfeSignInUserInformationCommand(email, firstname, lastname, startRequestResponse.RequestId, dfeSignInId));
                    return RedirectToAction("Index", "OrganisationSearch", new {requestId = startRequestResponse.RequestId});
                
                case DfeOrganisationCheckResult.Single:
                    await _mediator.Send(new AddDfESignInInformationCommand(dfeSignInId, dfeOrganisationsCheckResponse.Id, email, firstname, lastname, startRequestResponse.RequestId, dfeSignInId));
            
                    return RedirectToAction("Index", "CheckYourDetails", new {requestId = startRequestResponse.RequestId});
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}