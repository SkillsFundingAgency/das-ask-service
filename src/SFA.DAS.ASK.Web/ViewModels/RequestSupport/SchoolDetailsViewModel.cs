using System;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class SchoolDetailsViewModel
    {
        public SchoolDetailsViewModel(){ }
        
        public SchoolDetailsViewModel(SupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            County = supportRequest.County;
            SchoolName = supportRequest.OrganisationName;
        }

        [Required]
        public string SchoolName { get; set; }

        [Required]

        public string County { get; set; }

        public Guid RequestId { get; set; }

        public SupportRequest ToSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.OrganisationName = SchoolName;
            supportRequest.County = County;
            
            return supportRequest;
        }
    }
}