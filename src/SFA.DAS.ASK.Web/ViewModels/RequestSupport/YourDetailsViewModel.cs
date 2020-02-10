using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class YourDetailsViewModel
    {
        public YourDetailsViewModel() { }
        
        public YourDetailsViewModel(SupportRequest supportRequest)
        {
            FirstName = supportRequest.OrganisationContact.FirstName;
            LastName = supportRequest.OrganisationContact.LastName;
            JobRole = supportRequest.OrganisationContact.JobRole;
            PhoneNumber = supportRequest.OrganisationContact.PhoneNumber;
            Email = supportRequest.OrganisationContact.Email;
            ConfirmEmail = supportRequest.OrganisationContact.Email;
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
            supportRequest.OrganisationContact.FirstName = FirstName;
            supportRequest.OrganisationContact.LastName = LastName;
            supportRequest.OrganisationContact.JobRole = JobRole;
            supportRequest.OrganisationContact.PhoneNumber = PhoneNumber;
            supportRequest.OrganisationContact.Email = Email;

            return supportRequest;
        }
    }
}