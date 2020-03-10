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
            return await _askContext.PlanningMeetings.FirstOrDefaultAsync(pm => pm.Id == request.MeetingId, cancellationToken: cancellationToken);
        }
    }


}
