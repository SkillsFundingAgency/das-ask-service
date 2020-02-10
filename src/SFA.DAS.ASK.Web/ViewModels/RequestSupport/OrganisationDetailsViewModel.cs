using System;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OrganisationDetailsViewModel
    {
        public OrganisationDetailsViewModel(){ }
        
        public OrganisationDetailsViewModel(SupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            Other = supportRequest.Organisation.OtherOrganisationType;
            SelectedOrganisationType = supportRequest.Organisation.OrganisationType;
        }

        public Guid RequestId { get; set; }
        [Required(ErrorMessage = "Please select an Organisation Type")]
        public int? SelectedOrganisationType { get; set; }
        public string Other { get; set; }

        public SupportRequest ToSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.Organisation.OrganisationType = SelectedOrganisationType;
            supportRequest.Organisation.OtherOrganisationType = Other;
            
            return supportRequest;
        }
    }
}