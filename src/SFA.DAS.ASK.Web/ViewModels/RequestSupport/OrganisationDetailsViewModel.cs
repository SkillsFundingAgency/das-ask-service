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
            Other = supportRequest.OtherOrganisationType;
            SelectedOrganisationType = supportRequest.OrganisationType;
        }

        public Guid RequestId { get; set; }
        [Required(ErrorMessage = "Please select an Organisation Type")]
        public int? SelectedOrganisationType { get; set; }
        public string Other { get; set; }

        public SupportRequest ToSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.OrganisationType = SelectedOrganisationType;
            supportRequest.OtherOrganisationType = Other;
            
            return supportRequest;
        }
    }
}