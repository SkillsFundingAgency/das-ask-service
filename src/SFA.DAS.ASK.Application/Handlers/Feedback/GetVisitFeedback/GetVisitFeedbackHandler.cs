using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback
{
    public class GetVisitFeedbackHandler : IRequestHandler<GetVisitFeedbackRequest, VisitFeedback>
    {
        private readonly AskContext _dbContext;

        public GetVisitFeedbackHandler(AskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VisitFeedback> Handle(GetVisitFeedbackRequest request, CancellationToken cancellationToken)
        {
            return await _dbContext.VisitFeedback.SingleOrDefaultAsync(f => f.Id == request.FeedbackId);
        }
    }
}