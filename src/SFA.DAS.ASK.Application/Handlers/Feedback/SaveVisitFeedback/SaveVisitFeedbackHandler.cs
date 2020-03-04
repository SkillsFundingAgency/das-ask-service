using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SFA.DAS.ASK.Data;

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

    public static class ObjectCopier
    {
        public static T CloneJson<T>(this T source)
        {            
            if (ReferenceEquals(source, null))
            {
                return default;
            }

            var deserializeSettings = new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace};

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}