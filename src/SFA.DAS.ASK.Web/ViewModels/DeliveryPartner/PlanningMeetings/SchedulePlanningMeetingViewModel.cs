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

        
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public bool Edit { get; set; }
        public SchedulePlanningMeetingViewModel()
        {

        }
        public SchedulePlanningMeetingViewModel(PlanningMeeting meeting, SupportRequest supportRequest, bool edit)
        {
            MeetingId = meeting.Id;
            OrganisationName = supportRequest.Organisation.OrganisationName;
            Type = (MeetingType)meeting.MeetingType;
            Minutes = meeting.MeetingTimeAndDate.Value.Minute;
            Hours = meeting.MeetingTimeAndDate.Value.Hour;
            Day = meeting.MeetingTimeAndDate.Value.Day;
            Month = meeting.MeetingTimeAndDate.Value.Month;
            Year = meeting.MeetingTimeAndDate.Value.Year;

            Edit = edit;
        }

        public PlanningMeeting UpdatePlanningMeeting(PlanningMeeting planningMeeting)
        {
            planningMeeting.MeetingType = Type;
            planningMeeting.MeetingTimeAndDate = new DateTime(Year, Month, Day, Hours, Minutes, 0);

            return planningMeeting;
        }
    }

    
}
