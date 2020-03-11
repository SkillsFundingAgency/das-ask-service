using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Please enter a value for minutes")]
        [Range(0,60)]
        public int Minutes { get; set; }
        [Required(ErrorMessage = "Please enter a value for hours")]
        [Range(0,23,ErrorMessage = "Must be between 0 and 23")]
        public int Hours { get; set; }

        public SchedulePlanningMeetingViewModel()
        {

        }
        public SchedulePlanningMeetingViewModel(PlanningMeeting meeting, SupportRequest supportRequest)
        {
            MeetingId = meeting.Id;
            OrganisationName = supportRequest.Organisation.OrganisationName;
            Type = (MeetingType)meeting.MeetingType;
            Minutes = meeting.MeetingTimeAndDate.Value.Minute;
            Hours = meeting.MeetingTimeAndDate.Value.Hour;
        }

        public PlanningMeeting UpdatePlanningMeeting(PlanningMeeting planningMeeting)
        {
            planningMeeting.MeetingType = Type;
            planningMeeting.MeetingTimeAndDate = new DateTime(2020, 12, 12, Hours, Minutes, 0);
            

            return planningMeeting;
        }
    }

    
}
