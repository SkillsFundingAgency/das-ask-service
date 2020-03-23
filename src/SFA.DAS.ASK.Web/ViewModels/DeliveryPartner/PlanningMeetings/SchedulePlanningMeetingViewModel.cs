using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    [ValidateDate("Day","Month","Year", ErrorMessage = "Enter a real date")]
    public class SchedulePlanningMeetingViewModel
    {
        public Guid MeetingId { get; set; }

        public string OrganisationName { get; set; }
        public DateTime DateOfMeeting { get; set; }
        public DateTime TimeOfMeeting { get; set; }
        public MeetingType? Type { get; set; }

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
        public Guid SupportId { get; set; }
        public SchedulePlanningMeetingViewModel()
        {

        }
        public SchedulePlanningMeetingViewModel(PlanningMeeting meeting, SupportRequest supportRequest, bool edit)
        {
            MeetingId = meeting.Id;
            OrganisationName = supportRequest.Organisation.OrganisationName;
            Type = (MeetingType)meeting.MeetingType.GetValueOrDefault();

            Minutes = meeting.MeetingTimeAndDate.GetValueOrDefault().Minute;
            Hours = meeting.MeetingTimeAndDate.GetValueOrDefault().Hour;
            Day = meeting.MeetingTimeAndDate.GetValueOrDefault().Day;
            Month = meeting.MeetingTimeAndDate.GetValueOrDefault().Month;
            Year = meeting.MeetingTimeAndDate.GetValueOrDefault().Year;

            SupportId = meeting.SupportRequestId;
            Edit = edit;
        }

        public PlanningMeeting UpdatePlanningMeeting(PlanningMeeting planningMeeting)
        {
            planningMeeting.MeetingType = Type;
            planningMeeting.MeetingTimeAndDate = new DateTime(Year, Month, Day, Hours, Minutes, 0);

            return planningMeeting;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ValidateDate : ValidationAttribute
    {
        public ValidateDate(params string[] propertyList)
        {
        }

        public override bool IsValid(object value)
        {
            
            DateTime date;
            var vm = (SchedulePlanningMeetingViewModel)value;
            var Day = vm.Day;
            var Month = vm.Month;
            var Year = vm.Year;

            return DateTime.TryParse($"{Day}/{Month}/{Year}", out date);
        }
    }
}
