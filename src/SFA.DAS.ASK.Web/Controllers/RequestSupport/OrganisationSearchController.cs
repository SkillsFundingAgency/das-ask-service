using System;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using MediatR;
using System.Threading.Tasks;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.DfeApi;
using System.Collections.Generic;

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
        public IActionResult Index(Guid requestid)
        {
            var vm = new OrganisationSearchViewModel(requestid, null, null);
            return View("~/Views/RequestSupport/OrganisationSearch.cshtml", vm);
        }


        //[HttpPost("organisation-search/{requestId}")]
        //public async Task<IActionResult> Search(Guid requestid, OrganisationSearchViewModel viewModel)
        //{

        //   var nonDfeOrganisations = await _mediator.Send(new GetNonDfeOrganisationsRequest());


        //    viewModel.Results = nonDfeOrganisations;
        //    viewModel.RequestId = requestid;
        //    return RedirectToAction("Index", "OrganisationResults", new { requestid = requestid, search = viewModel.SearchTerms });

        //    return View("~/Views/RequestSupport/OrganisationSearch.cshtml", viewModel);
        //}
    }

    public class OrganisationSearchViewModel
    {
        public Guid RequestId { get; set; }
        public string SearchTerms { get; set; }
        public List<NonDfeOrganisation> Results { get; set; }

        public OrganisationSearchViewModel() { }
        public OrganisationSearchViewModel(Guid requestId, string searchTerms,List<NonDfeOrganisation> results)
        {
            RequestId = requestId;
            SearchTerms = searchTerms;
            Results = results;
        }
    }
}