using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.Feedback.GetVisitFeedbackTests
{
    [TestFixture]
    public class WhenGetVisitFeedbackHandled
    {
        [Test]
        public async Task ThenTheCorrectVisitFeedbackIsReturned()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var requestedFeedbackId = Guid.NewGuid();
            await dbContext.VisitFeedback.AddRangeAsync(new List<VisitFeedback>
            {
                new VisitFeedback{Status = FeedbackStatus.NotStarted, Id = Guid.NewGuid()},
                new VisitFeedback{Status = FeedbackStatus.NotStarted, Id = requestedFeedbackId},
            });
            await dbContext.SaveChangesAsync();
            
            var handler = new GetVisitFeedbackHandler(dbContext);
            var result = await handler.Handle(new GetVisitFeedbackRequest(requestedFeedbackId), CancellationToken.None);
            
            result.Id.Should().Be(requestedFeedbackId);
        }
    }
}