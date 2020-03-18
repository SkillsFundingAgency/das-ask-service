using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class LogContactViewModel
    {
        public Guid RequestId { get; set; }
        public String EstablishmentName { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
       
        public bool SelectedContactMethod { get; set; }
        public bool Telephone { get; set; }
        public bool Email { get; set; }
        public bool SchedulePlanningMeeting { get; set; }
        public Status Status { get; set; }
        public DateTime ContactedDate { get; set; }

        public LogContactViewModel()
        {

        }
        public LogContactViewModel(SupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            Status = supportRequest.CurrentStatus;
            EstablishmentName = supportRequest.Organisation.OrganisationName;

        }

        public SupportRequest UpdateSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.CurrentStatus = Status.ContactConfirmed;
            supportRequest.ContactedDate = new DateTime(Year, Month, Day);

            return supportRequest;
        }

        
    }
}
