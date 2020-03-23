using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting
{
    public class GetPlanningMeetingRequest : IRequest<PlanningMeeting>
    {
        public Guid RequestId { get; set; }

        public GetPlanningMeetingRequest(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}
