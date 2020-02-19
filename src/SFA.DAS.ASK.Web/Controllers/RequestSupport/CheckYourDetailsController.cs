using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class CheckYourDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public CheckYourDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("check-your-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var tempSupportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));

            var vm = new CheckYourDetailsViewModel(tempSupportRequest);

            return View("~/Views/RequestSupport/CheckYourDetails.cshtml", vm);
        }

        [HttpPost("check-your-details/{requestId}")]
        public async Task<IActionResult> Continue(Guid requestId)
        {
            return RedirectToAction("Index", "OtherDetails", new {requestId});
        }
    }
}