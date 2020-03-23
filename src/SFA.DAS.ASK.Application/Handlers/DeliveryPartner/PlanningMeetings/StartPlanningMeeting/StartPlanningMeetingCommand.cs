using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.StartPlanningMeeting
{
    public class StartPlanningMeetingCommand : IRequest<StartPlanningMeetingResponse>
    {
        public Guid RequestId { get; set; }
        public Guid DeliveryPartnerId { get; set; }
        public MeetingType MeetingType { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public StartPlanningMeetingCommand(Guid requestId, Guid deliveryPartnerId, int day, int month, int year, int hours, int minutes, MeetingType meetingType)
        {
            RequestId = requestId;
            DeliveryPartnerId = deliveryPartnerId;
            Day = day;
            Month = month;
            Year = year;
            Hours = hours;
            Minutes = minutes;
            MeetingType = meetingType;
        }
    }
}
