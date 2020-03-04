using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.Feedback
{
    public class ConfirmDetailsViewModel
    {
        public string EstablishmentName { get; set; }
        public string YourFullName { get; set; }
        public string DateOfActivity { get; set; }
        public List<VisitActivity> Activities { get; set; }
        public Guid FeedbackId { get; set; }

        public ConfirmDetailsViewModel(VisitFeedback fullVisit)
        {
            FeedbackId = fullVisit.Id;
            var visit = fullVisit.Visit;

            EstablishmentName = visit.SupportRequest.Organisation.OrganisationName;
            YourFullName = $"{visit.SupportRequest.OrganisationContact.FirstName} {visit.SupportRequest.OrganisationContact.LastName}";
            DateOfActivity = visit.VisitDate.ToString("dd/MM/yyyy");
            Activities = visit.Activities;
            
        }
    }
}
