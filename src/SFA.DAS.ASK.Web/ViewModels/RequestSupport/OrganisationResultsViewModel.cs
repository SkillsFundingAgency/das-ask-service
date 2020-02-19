using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OrganisationResultsViewModel
    {
        public Guid RequestId { get; set; }
        public List<ReferenceDataSearchResult> Results { get; set; }
        public string Search { get; set; }

        public OrganisationResultsViewModel() { }
        public OrganisationResultsViewModel(List<ReferenceDataSearchResult> results, Guid requestId, string searchTerms)
        {
            Results = results;
            RequestId = requestId;
            Search = searchTerms;
        }
        public Guid SelectedResult { get; set; }

        
    }
}
