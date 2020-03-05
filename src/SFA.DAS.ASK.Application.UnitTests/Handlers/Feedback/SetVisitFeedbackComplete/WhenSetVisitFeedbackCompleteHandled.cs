using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitComplete;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.Feedback.SetVisitFeedbackComplete
{
    [TestFixture]
    public class WhenSetVisitFeedbackCompleteHandled
    {
        [Test]
        public async Task ThenVisitStatusSetToComplete()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var requestFeedbackId = Guid.NewGuid();

            await dbContext.VisitFeedback.AddAsync(new VisitFeedback(){ Id = requestFeedbackId, Status = FeedbackStatus.NotStarted });
            await dbContext.SaveChangesAsync();

            var handler = new SetVisitFeedbackCompleteHandler(dbContext);
            var updatedDb = await handler.Handle(new SetVisitFeedbackCompleteCommand(requestFeedbackId, FeedbackStatus.Complete), CancellationToken.None);

            dbContext.VisitFeedback.Where(visit => visit.Id == requestFeedbackId).FirstOrDefault().Id.Should().Be(requestFeedbackId); 
        }
    }
}
