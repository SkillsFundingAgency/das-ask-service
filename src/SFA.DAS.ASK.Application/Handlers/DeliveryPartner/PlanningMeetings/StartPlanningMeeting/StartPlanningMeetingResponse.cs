using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.StartPlanningMeeting
{
    public class StartPlanningMeetingResponse
    {
        public Guid MeetingId { get; set; }

        public StartPlanningMeetingResponse(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}
