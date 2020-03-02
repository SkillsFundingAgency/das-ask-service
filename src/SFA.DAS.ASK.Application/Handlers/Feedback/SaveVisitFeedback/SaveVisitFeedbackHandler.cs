using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.SaveVisitFeedback
{
    public class SaveVisitFeedbackHandler : IRequestHandler<SaveVisitFeedbackRequest>
    {
        private readonly AskContext _dbContext;

        public SaveVisitFeedbackHandler(AskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SaveVisitFeedbackRequest request, CancellationToken cancellationToken)
        {
            var feedback = await _dbContext.VisitFeedback.SingleAsync(f => f.Id == request.FeedbackId, CancellationToken.None);
            feedback.FeedbackAnswers = request.FeedbackAnswers;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public class SaveVisitFeedbackRequest : IRequest
    {
        public Guid FeedbackId { get; }
        public FeedbackAnswers FeedbackAnswers { get; }

        public SaveVisitFeedbackRequest(Guid feedbackId, FeedbackAnswers feedbackAnswers)
        {
            FeedbackId = feedbackId;
            FeedbackAnswers = feedbackAnswers;
        }
    }
}