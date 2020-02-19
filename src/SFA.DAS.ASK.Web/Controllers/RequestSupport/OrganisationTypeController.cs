using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class OrganisationTypeController : Controller
    {
        private readonly IMediator _mediator;

        public OrganisationTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("organisation-type/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            var vm = new OrganisationTypeViewModel(supportRequest);

            return View("~/Views/RequestSupport/OrganisationDetails.cshtml", vm);
        }

        [HttpPost("organisation-type/{requestId}")]
        public async Task<IActionResult> Index(Guid requestId, OrganisationTypeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/RequestSupport/OrganisationDetails.cshtml", viewModel);
            }

            if (viewModel.SelectedOrganisationType == OrganisationType.Other && string.IsNullOrWhiteSpace(viewModel.Other))
            {
                ModelState.AddModelError("organisationType_other_details", "Please enter something for Other");
                return View("~/Views/RequestSupport/OrganisationDetails.cshtml", viewModel);
            }
            
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            await _mediator.Send(new SaveTempSupportRequest(viewModel.ToSupportRequest(supportRequest)));
           
            return RedirectToAction("Index", "OrganisationSearch", new {requestId = requestId});
        }
    }
}