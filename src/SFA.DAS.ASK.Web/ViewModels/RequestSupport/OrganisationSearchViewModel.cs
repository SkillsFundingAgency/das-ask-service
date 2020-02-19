using SFA.DAS.ASK.Application.DfeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{

    public class OrganisationSearchViewModel
    {
        public Guid RequestId { get; set; }
        public string Search { get; set; }
        public List<NonDfeOrganisation> Results { get; set; }

        public OrganisationSearchViewModel() { }
        public OrganisationSearchViewModel(Guid requestId, string searchTerms, List<NonDfeOrganisation> results)
        {
            RequestId = requestId;
            Search = searchTerms;
            Results = results;
        }
    }
}
