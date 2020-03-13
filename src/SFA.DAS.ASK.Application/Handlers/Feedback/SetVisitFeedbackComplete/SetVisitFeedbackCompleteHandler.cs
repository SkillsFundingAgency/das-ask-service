using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitComplete;
using SFA.DAS.ASK.Application.Services.Email;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitFeedbackComplete
{
    public class SetVisitFeedbackCompleteHandler : IRequestHandler<SetVisitFeedbackCompleteCommand>
    {
        private readonly AskContext _dbContext;
        private readonly IEmailService _emailService;

        public SetVisitFeedbackCompleteHandler(AskContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(SetVisitFeedbackCompleteCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _dbContext.VisitFeedback
                .Include(f => f.Visit.OrganisationContact)
                .Include(f => f.Visit.SupportRequest.Organisation)
                .SingleAsync(f => f.Id == request.FeedbackId, CancellationToken.None);
            
            feedback.Status = request.FeedbackStatus;
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _emailService.SendFeedbackSubmitted(feedback.Visit.OrganisationContact.Email, feedback.Visit.OrganisationContact.FirstName, feedback.Visit.SupportRequest.Organisation.OrganisationName);
                
            return Unit.Value;
        }
    }
}
