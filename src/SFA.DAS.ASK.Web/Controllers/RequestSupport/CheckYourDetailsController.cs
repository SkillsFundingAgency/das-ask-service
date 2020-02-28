using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.Filters;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class CheckYourDetailsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public CheckYourDetailsController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
        }

        [HttpGet("check-your-details/{requestId}")]
        [ServiceFilter(typeof(CheckRequestFilter))]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var tempSupportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));

            var searchTerms = _sessionService.Get($"Searchstring-{requestId}");

            var vm = new CheckYourDetailsViewModel(tempSupportRequest, searchTerms);

            return View("~/Views/RequestSupport/CheckYourDetails.cshtml", vm);
        }

        [HttpPost("check-your-details/{requestId}")]
        public IActionResult Continue(Guid requestId)
        {
            return RedirectToAction("Index", "OtherDetails", new {requestId});
        }
    }
}