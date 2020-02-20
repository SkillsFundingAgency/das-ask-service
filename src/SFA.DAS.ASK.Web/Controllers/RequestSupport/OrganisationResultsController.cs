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
            if (string.IsNullOrEmpty(search))
            {
                search = _sessionService.Get("Searchstring-" + requestId.ToString());

                var cachedResults = JsonConvert.DeserializeObject < List < ReferenceDataSearchResult >> (_sessionService.Get(requestId.ToString()));

                var viewModel = new OrganisationResultsViewModel(cachedResults, requestId, search);

                return View("~/Views/RequestSupport/OrganisationResults.cshtml", viewModel);
            } 
            else
            {
                _sessionService.Set("Searchstring-" + requestId.ToString(), search);

                var nonDfeOrganisations = await _mediator.Send(new GetNonDfeOrganisationsRequest(search));

                var nonDfeOrganisationsList = nonDfeOrganisations.ToList();

                nonDfeOrganisationsList.ForEach(o => o.Id = Guid.NewGuid());


                _sessionService.Set(requestId.ToString(), JsonConvert.SerializeObject(nonDfeOrganisationsList));
                var viewModel = new OrganisationResultsViewModel(nonDfeOrganisationsList, requestId, search);

                return View("~/Views/RequestSupport/OrganisationResults.cshtml", viewModel);
            }

           
        }

        [HttpPost("organisation-results/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, OrganisationResultsViewModel viewModel)
        {
            
            // Model Validation ??

            var cachedResults = JsonConvert.DeserializeObject<List<ReferenceDataSearchResult>>(_sessionService.Get(requestId.ToString()));

            var selectedResult = cachedResults.Where(r => r.Id == viewModel.SelectedResult).FirstOrDefault();

            await _mediator.Send(new AddNonDfESignInInformationCommand(selectedResult, requestId));

            // handle failed saves??

            return RedirectToAction("Index", "CheckYourDetails", new { requestId });
        }

        [HttpGet("organisation-results/{requestId}/{selectedSchool}")]
        [ExportModelState]
        public async Task<IActionResult> Select(Guid requestId, Guid selectedSchool)
        {

            // Model Validation ??

            var cachedResults = JsonConvert.DeserializeObject<List<ReferenceDataSearchResult>>(_sessionService.Get(requestId.ToString()));

            var selectedResult = cachedResults.Where(r => r.Id == selectedSchool).FirstOrDefault();

            await _mediator.Send(new AddNonDfESignInInformationCommand(selectedResult, requestId));

            // handle failed saves??

            return RedirectToAction("Index", "CheckYourDetails", new { requestId });
        }
    }
}