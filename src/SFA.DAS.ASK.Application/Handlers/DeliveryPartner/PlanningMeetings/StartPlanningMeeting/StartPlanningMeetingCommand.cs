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

        public StartPlanningMeetingCommand(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}
