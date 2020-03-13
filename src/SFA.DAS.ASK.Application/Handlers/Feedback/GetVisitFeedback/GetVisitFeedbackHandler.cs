using System.Linq;
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
            var feedback = _dbContext.VisitFeedback.Where(f => f.Id == request.FeedbackId);
                
            if (request.IncludeSubTables)
            {
                feedback = feedback
                    .Include(vf => vf.Visit.Activities)
                    .Include(vf => vf.Visit.SupportRequest.Organisation)
                    .Include(vf => vf.Visit.OrganisationContact);
            }
            
            return await feedback.SingleOrDefaultAsync(cancellationToken);
        }
    }
}