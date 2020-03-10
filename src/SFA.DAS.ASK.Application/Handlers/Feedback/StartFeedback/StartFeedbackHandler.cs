using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.StartFeedback
{
    public class StartFeedbackHandler : IRequestHandler<StartFeedbackCommand>
    {
        private readonly AskContext _context;

        public StartFeedbackHandler(AskContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(StartFeedbackCommand request, CancellationToken cancellationToken)
        {
            var visitFeedback = await _context.VisitFeedback.SingleAsync(vf => vf.Id == request.FeedbackId, cancellationToken: cancellationToken);

            if (visitFeedback.Status == FeedbackStatus.NotStarted)
            {
                visitFeedback.Status = FeedbackStatus.InProgress;    
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}