using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetDfeOrganisations;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class SelectOrganisationController : Controller
    {
        private readonly IMediator _mediator;

        public SelectOrganisationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("select-organisation/{requestId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var tempSupportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            
            var dfeOrganisations = await _mediator.Send(new GetDfeOrganisationsRequest(tempSupportRequest.DfeSignInId.Value));
            
            var viewModel = new SelectOrganisationViewModel(dfeOrganisations, requestId, tempSupportRequest.SelectedDfeSignInOrganisationId);

            return View("~/Views/RequestSupport/SelectOrganisation.cshtml", viewModel);
        }
        
        [HttpPost("select-organisation/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, SelectOrganisationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new {requestId});
            }
            
            await _mediator.Send(new AddDfESignInOrganisationCommand(requestId, viewModel.SelectedId.Value));
            
            return RedirectToAction("Index", "CheckYourDetails", new {requestId = requestId});
        }
    }
}