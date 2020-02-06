using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class OrganisationDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public OrganisationDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("organisation-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            var vm = new OrganisationDetailsViewModel(supportRequest);

            return View("~/Views/RequestSupport/OrganisationDetails.cshtml", vm);
        }

        [HttpPost("organisation-details/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId, OrganisationDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/RequestSupport/OrganisationDetails.cshtml", viewModel);
            }

            if (viewModel.SelectedOrganisationType == 6 && string.IsNullOrWhiteSpace(viewModel.Other))
            {
                ModelState.AddModelError("organisationType_other_details", "Please enter something for Other");
                return View("~/Views/RequestSupport/OrganisationDetails.cshtml", viewModel);
            }
            
            var supportRequest = await _mediator.Send(new GetSupportRequest(requestId));
            
            await _mediator.Send(new SaveSupportRequest(viewModel.ToSupportRequest(supportRequest)));

            if (viewModel.SelectedOrganisationType == 1) // School
            {
                return RedirectToAction("Index", "SchoolDetails", new {requestId = requestId});
            }
            
            return RedirectToAction("Index", "OrganisationAddress", new {requestId = requestId});
        }
    }
}