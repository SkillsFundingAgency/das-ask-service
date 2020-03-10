using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class SchedulePlanningMeetingViewModel
    {
        public Guid MeetingId { get; set; }

        public string OrganisationName { get; set; }
        public DateTime DateOfMeeting { get; set; }
        public DateTime TimeOfMeeting { get; set; }
        public MeetingType Type { get; set; }

        public SchedulePlanningMeetingViewModel(PlanningMeeting meeting)
        {
            MeetingId = meeting.Id;
            //TimeOfMeeting = meeting.
        }
    }

    
}
