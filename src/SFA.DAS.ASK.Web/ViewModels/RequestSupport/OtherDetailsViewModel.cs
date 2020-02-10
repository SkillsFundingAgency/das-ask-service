using System;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OtherDetailsViewModel
    {
        public OtherDetailsViewModel() { }
        
        public OtherDetailsViewModel(SupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            AdditionalComments = supportRequest.AdditionalComments;
            Agree = supportRequest.Agree;
            Email = supportRequest.OrganisationContact.Email;
        }

        public string AdditionalComments { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms....")]
        
        public bool Agree { get; set; }

        public Guid RequestId { get; set; }

        public SupportRequest ToSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.AdditionalComments = AdditionalComments;
            supportRequest.Agree = Agree;
            
            return supportRequest;
        }

        public bool NonSignedIn { get; set; }
        public string Email { get; set; }
    }
}