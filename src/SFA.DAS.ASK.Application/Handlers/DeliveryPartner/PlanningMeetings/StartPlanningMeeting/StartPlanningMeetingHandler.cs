using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.StartPlanningMeeting
{
    public class StartPlanningMeetingHandler : IRequestHandler<StartPlanningMeetingCommand, StartPlanningMeetingResponse>
    {
        private readonly AskContext _askContext;

        public StartPlanningMeetingHandler(AskContext askContext)
        {
            _askContext = askContext;
        }
        public async Task<StartPlanningMeetingResponse> Handle(StartPlanningMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetingId = Guid.NewGuid();

            _askContext.PlanningMeetings.Add(new PlanningMeeting()
            {
                Id = meetingId,
                SupportRequestId = Guid.NewGuid(),
                ContactId = Guid.NewGuid(),
                DeliveryPartnerId = Guid.NewGuid(),
                MeetingType = MeetingType.FaceToFace

            });

            await _askContext.SaveChangesAsync(cancellationToken);

            return new StartPlanningMeetingResponse(meetingId);
        }
    }
}
