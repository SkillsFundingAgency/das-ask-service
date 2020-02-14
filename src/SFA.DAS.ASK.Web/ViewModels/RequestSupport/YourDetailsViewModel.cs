using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class YourDetailsViewModel
    {
        public YourDetailsViewModel() { }
        
        public YourDetailsViewModel(TempSupportRequest supportRequest)
        {
            FirstName = supportRequest.FirstName;
            LastName = supportRequest.LastName;
            JobRole = supportRequest.JobRole;
            PhoneNumber = supportRequest.PhoneNumber;
            Email = supportRequest.Email;
            ConfirmEmail = supportRequest.Email;
        }

        [Required(ErrorMessage = "Enter your first name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Enter your last name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Enter your job role")]
        public string JobRole { get; set; }
        
        [Required(ErrorMessage = "Enter your phone number")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Enter your email address")]
        [EmailAddress(ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Confirm your email address")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Email addresses must match")]
        public string ConfirmEmail { get; set; }

        public TempSupportRequest ToTempSupportRequest(TempSupportRequest supportRequest)
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