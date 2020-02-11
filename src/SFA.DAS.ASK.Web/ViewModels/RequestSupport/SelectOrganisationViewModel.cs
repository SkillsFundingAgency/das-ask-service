using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SFA.DAS.ASK.Application.DfeApi;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class SelectOrganisationViewModel
    {
        public Guid RequestId { get; }
        public SelectOrganisationViewModel() { }
        
        public SelectOrganisationViewModel(List<DfeOrganisation> dfeOrganisations, Guid requestId)
        {
            RequestId = requestId;
            Organisations = dfeOrganisations.ToDictionary(organisation => organisation.Urn, organisation => organisation.Name);
        }

        public Dictionary<string, string> Organisations { get; set; }
        [Required(ErrorMessage = "Please select one of your Organisations")]
        public string SelectedUrn { get; set; }
    }
}