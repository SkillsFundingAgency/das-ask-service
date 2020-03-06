using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.AddAmmendmentComment
{
    public class AddAmmendmentCommentHandler : IRequestHandler<AddAmmendmentCommentCommand>
    {
        private readonly AskContext _dbContext;

        public AddAmmendmentCommentHandler(AskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddAmmendmentCommentCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _dbContext.VisitFeedback.SingleAsync(f => f.Id == request.FeedbackId, CancellationToken.None);
            feedback.IncorrectDetailsComments = request.Comment;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
