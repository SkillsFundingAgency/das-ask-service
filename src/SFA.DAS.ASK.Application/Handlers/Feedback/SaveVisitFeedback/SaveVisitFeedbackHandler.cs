using System;
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
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace};

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}