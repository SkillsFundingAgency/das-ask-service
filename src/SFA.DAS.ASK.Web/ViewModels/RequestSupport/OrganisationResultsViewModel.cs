using SFA.DAS.ASK.Application.DfeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OrganisationResultsViewModel
    {
        public Guid RequestId { get; set; }
        public List<NonDfeOrganisation> Results { get; set; }
        public string SearchTerms { get; set; }

        public OrganisationResultsViewModel() { }
        public OrganisationResultsViewModel(List<NonDfeOrganisation> results, Guid requestId, string searchTerms)
        {
            Results = results;
            RequestId = requestId;
            SearchTerms = searchTerms;
        }
        public Guid SelectedResult { get; set; }
    }
}
