using SFA.DAS.ASK.Application.Services.DfeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.ASK.Application.Utils;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{

    public class OrganisationSearchViewModel
    {
        public Guid RequestId { get; set; }
        public string Search { get; set; }
        public string OrganisationType { get; set; }

        public OrganisationSearchViewModel() { }
        public OrganisationSearchViewModel(TempSupportRequest request, string searchTerms)
        {
            RequestId = request.Id;
            OrganisationType = EnumHelper.GetEnumDescription(request.OrganisationType);
            Search = searchTerms;
        }
    }
}
