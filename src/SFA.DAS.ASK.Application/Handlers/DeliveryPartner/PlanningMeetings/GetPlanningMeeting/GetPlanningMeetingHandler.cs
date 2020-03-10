using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting
{
    public class GetPlanningMeetingHandler : IRequestHandler<GetPlanningMeetingRequest, PlanningMeeting>
    {
        private readonly AskContext _askContext;

        public GetPlanningMeetingHandler(AskContext askContext)
        {
            _askContext = askContext;
        }
        public async Task<PlanningMeeting> Handle(GetPlanningMeetingRequest request, CancellationToken cancellationToken)
        {
            var all = _askContext;
                var meetings = _askContext.PlanningMeetings;
            var requests = _askContext.SupportRequests;
            var meeting = await _askContext.PlanningMeetings.FirstOrDefaultAsync(pm => pm.Id == request.MeetingId, cancellationToken: cancellationToken);

            return meeting;
        }
    }


}
