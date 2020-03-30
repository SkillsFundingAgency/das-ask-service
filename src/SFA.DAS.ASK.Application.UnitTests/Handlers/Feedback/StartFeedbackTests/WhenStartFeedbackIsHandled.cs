using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.StartFeedback;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.Feedback.StartFeedbackTests
{
    [TestFixture]
    public class WhenStartFeedbackIsHandled
    {
        [Test]
        public async Task AndCurrentStatusIsNotStarted_ThenStatusIsSetToInProgress()
        {
            var dbContext = ContextHelper.GetInMemoryContext();

            var feedbackId = Guid.NewGuid();
            await dbContext.VisitFeedback.AddAsync(new VisitFeedback
            {
                Id = feedbackId, 
                Status = FeedbackStatus.NotStarted
            });
            
            await dbContext.SaveChangesAsync();
            
            var handler = new StartFeedbackHandler(dbContext);

            await handler.Handle(new StartFeedbackCommand(feedbackId),CancellationToken.None);

            var savedFeedback = await dbContext.VisitFeedback.SingleAsync();
            savedFeedback.Status.Should().Be(FeedbackStatus.InProgress);
        }
        
        [Test]
        public async Task AndCurrentStatusIsNOTNotStarted_ThenStatusIsNotChanged()
        {
            var dbContext = ContextHelper.GetInMemoryContext();

            var feedbackId = Guid.NewGuid();
            await dbContext.VisitFeedback.AddAsync(new VisitFeedback
            {
                Id = feedbackId, 
                Status = FeedbackStatus.Complete
            });
            
            await dbContext.SaveChangesAsync();
            
            var handler = new StartFeedbackHandler(dbContext);

            await handler.Handle(new StartFeedbackCommand(feedbackId),CancellationToken.None);

            var savedFeedback = await dbContext.VisitFeedback.SingleAsync();
            savedFeedback.Status.Should().Be(FeedbackStatus.Complete);
        }
    }
}