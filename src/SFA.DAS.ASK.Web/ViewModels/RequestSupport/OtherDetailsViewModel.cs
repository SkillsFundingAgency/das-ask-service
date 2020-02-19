using System;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OtherDetailsViewModel
    {
        public OtherDetailsViewModel() { }
        
        public OtherDetailsViewModel(TempSupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            AdditionalComments = supportRequest.AdditionalComments;
            Agree = supportRequest.Agree;
            Email = supportRequest.Email;
            DisplayName = supportRequest.FirstName + " " + supportRequest.LastName;
            SupportRequestType = supportRequest.SupportRequestType;
        }

        public SupportRequestType SupportRequestType { get; set; }

        public string AdditionalComments { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms....")]
        
        public bool Agree { get; set; }

        public Guid RequestId { get; set; }

        public TempSupportRequest ToTempSupportRequest(TempSupportRequest supportRequest)
        {
            supportRequest.AdditionalComments = AdditionalComments;
            supportRequest.Agree = Agree;
            
            return supportRequest;
        }

        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}