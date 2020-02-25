using System;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using MediatR;
using System.Threading.Tasks;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Services.DfeApi;
using System.Collections.Generic;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class OrganisationSearchController : Controller
    {
        private readonly IMediator _mediator;

        public OrganisationSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("organisation-search/{requestId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid requestId, string search)
        {
            var tempSupportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));
            var vm = new OrganisationSearchViewModel(tempSupportRequest, search);
            return View("~/Views/RequestSupport/OrganisationSearch.cshtml", vm);
        }

        [HttpPost("organisation-search/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Search(Guid requestId, OrganisationSearchViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "OrganisationSearch", new { requestId = requestId });
            }

            return RedirectToAction("Index", "OrganisationResults", new { requestId = requestId, search = viewmodel.Search });
        }
    }
}