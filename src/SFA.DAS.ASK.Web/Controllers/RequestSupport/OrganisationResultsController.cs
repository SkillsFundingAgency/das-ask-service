using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class OrganisationResultsController : Controller
    {
        private readonly IMediator _mediator;

        public OrganisationResultsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("organisation-results/{requestId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid requestId, string search)
        {

            var nonDfeOrganisations = await _mediator.Send(new GetNonDfeOrganisationsRequest());

            nonDfeOrganisations.ForEach(o => o.Guid = Guid.NewGuid());

            // cache results


            var viewModel = new OrganisationResultsViewModel(nonDfeOrganisations, requestId, search);

            return View("~/Views/RequestSupport/OrganisationResults.cshtml", viewModel);
        }

        [HttpPost("organisation-results/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, OrganisationResultsViewModel viewModel)
        {
            //post back selected guid
            // Model Validation ??
            var view = viewModel;
            // lookup from cache for guid
            var selectedResult = viewModel.Results.First(r => r.Guid == viewModel.SelectedResult);

            // save to db

            // redirect to check your answers

            return View("~/Views/RequestSupport/OrganisationResults.cshtml", viewModel);
        }
    }
}