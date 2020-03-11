using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitComplete
{
    public class SetVisitFeedbackCompleteHandler : IRequestHandler<SetVisitFeedbackCompleteCommand>
    {
        private readonly AskContext _dbContext;

        public SetVisitFeedbackCompleteHandler(AskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SetVisitFeedbackCompleteCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _dbContext.VisitFeedback.SingleAsync(f => f.Id == request.FeedbackId, CancellationToken.None);
            feedback.Status = request.FeedbackStatus;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
