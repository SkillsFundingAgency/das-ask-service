using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddNonDfeSignInInformation;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using SFA.DAS.ASK.Application.Services.Session;
using Newtonsoft.Json;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using System.Threading;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSelectedOrganisationSearchResult;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class OrganisationResultsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public OrganisationResultsController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
        }

        [HttpGet("organisation-results/{requestId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid requestId, string search, bool edit)
        {
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            var fromCache = false;

            if (string.IsNullOrEmpty(search))
            {
                search = _sessionService.Get($"Searchstring-{requestId}");
                fromCache = true;

                if (string.IsNullOrWhiteSpace(search))
                {
                    return RedirectToAction("Index", "OrganisationSearch", new { requestId = requestId });
                }
            }

            var results = (await _mediator.Send(new GetNonDfeOrganisationsRequest(search, requestId, fromCache), default(CancellationToken))).ToList();

            var viewModel = new OrganisationResultsViewModel(results, requestId, search, edit, supportRequest.ReferenceId);

            return View("~/Views/RequestSupport/OrganisationResults.cshtml", viewModel);
        }

        [HttpPost("organisation-results/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, OrganisationResultsViewModel viewModel)
        {
            var selectedResult = await _mediator.Send(new GetSelectedOrganisationSearchResultRequest(viewModel.SelectedResult, requestId));

            await _mediator.Send(new AddNonDfESignInInformationCommand(selectedResult, requestId));

            return RedirectToAction("Index", "CheckYourDetails", new { requestId });
        }

    }
}