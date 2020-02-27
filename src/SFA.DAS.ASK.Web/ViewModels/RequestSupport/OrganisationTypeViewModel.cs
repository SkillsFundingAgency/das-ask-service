using SFA.DAS.ASK.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OrganisationTypeViewModel
    {
        public OrganisationTypeViewModel(){ }
        
        public OrganisationTypeViewModel(TempSupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            Other = supportRequest.OtherOrganisationType;
            SelectedOrganisationType = supportRequest.OrganisationType;
        }

        public Guid RequestId { get; set; }
        [Required(ErrorMessage = "Please select an organisation type")]
        public OrganisationType? SelectedOrganisationType { get; set; }
        
        public string Other { get; set; }

        public TempSupportRequest UpdateTempSupportRequest(TempSupportRequest supportRequest)
        {
            supportRequest.OrganisationType = SelectedOrganisationType;
            supportRequest.OtherOrganisationType = Other;
            
            return supportRequest;
        }
    }
}