using System;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class OrganisationSearchController : Controller
    {
        public IActionResult Index(Guid requestid)
        {
            var vm = new OrganisationSearchViewModel(requestid);
            return View("~/Views/RequestSupport/OrganisationSearch.cshtml", vm);
        }
    }

    public class OrganisationSearchViewModel
    {
        public Guid RequestId { get; }

        public OrganisationSearchViewModel(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}