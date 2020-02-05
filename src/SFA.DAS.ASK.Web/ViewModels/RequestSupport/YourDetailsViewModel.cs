using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class YourDetailsViewModel
    {
        public YourDetailsViewModel() { }
        
        public YourDetailsViewModel(SupportRequest supportRequest)
        {
            FirstName = supportRequest.FirstName;
            LastName = supportRequest.LastName;
            JobRole = supportRequest.JobRole;
            PhoneNumber = supportRequest.PhoneNumber;
            Email = supportRequest.Email;
            ConfirmEmail = supportRequest.Email;
        }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string JobRole { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [EmailAddress]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }

        public SupportRequest ToSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.FirstName = FirstName;
            supportRequest.LastName = LastName;
            supportRequest.JobRole = JobRole;
            supportRequest.PhoneNumber = PhoneNumber;
            supportRequest.Email = Email;

            return supportRequest;
        }
    }
}