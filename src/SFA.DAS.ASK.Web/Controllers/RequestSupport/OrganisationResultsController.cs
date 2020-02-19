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
using SFA.DAS.ASK.Application.DfeApi;

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
        public async Task<IActionResult> Index(Guid requestId, string search)
        {

            var nonDfeOrganisations = await _mediator.Send(new GetNonDfeOrganisationsRequest());

            nonDfeOrganisations.ForEach(o => o.Guid = Guid.NewGuid());

            _sessionService.Set(requestId.ToString(), JsonConvert.SerializeObject(nonDfeOrganisations));

            var viewModel = new OrganisationResultsViewModel(nonDfeOrganisations, requestId, search);

            return View("~/Views/RequestSupport/OrganisationResults.cshtml", viewModel);
        }

        [HttpPost("organisation-results/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, OrganisationResultsViewModel viewModel)
        {
            
            // Model Validation ??

            var cachedResults = JsonConvert.DeserializeObject<List<NonDfeOrganisation>>(_sessionService.Get(requestId.ToString()));

            var selectedResult = cachedResults.Where(r => r.Guid == viewModel.SelectedResult).FirstOrDefault();

            await _mediator.Send(new AddNonDfESignInInformationCommand(selectedResult, requestId));

            // handle failed saves??

            return RedirectToAction("Index", "CheckAnswers", new { requestId });
        }
    }
}