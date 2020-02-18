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
        
        // [HttpGet("sign-in")]
        // [Authorize]
        // public IActionResult SignIn()
        // {
        //     return View("~/Views/RequestSupport/SignIn.cshtml");
        // }
    }
}