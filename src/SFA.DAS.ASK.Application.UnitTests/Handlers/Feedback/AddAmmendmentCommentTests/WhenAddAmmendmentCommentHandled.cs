using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.AddAmmendmentComment;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.Feedback.AddAmmendmentCommentTests
{
    [TestFixture]
    public class WhenAddAmmendmentCommentHandled
    {
        private string ADDITIONAL_COMMENT;

        [SetUp]
        public void Arrange()
        {
            ADDITIONAL_COMMENT = "Added Comment";
        }
        [Test]
        public async Task ThenAmmendmentCommentIsAdded()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var requestFeedbackId = Guid.NewGuid();

            await dbContext.VisitFeedback.AddAsync(new VisitFeedback() { Id = requestFeedbackId, Status = FeedbackStatus.NotStarted, IncorrectDetailsComments = null});
            await dbContext.SaveChangesAsync();

            var handler = new AddAmmendmentCommentHandler(dbContext);
            var updatedDb = await handler.Handle(new AddAmmendmentCommentCommand(requestFeedbackId, ADDITIONAL_COMMENT), CancellationToken.None);

            dbContext.VisitFeedback.Where(visit => visit.Id == requestFeedbackId).FirstOrDefault().IncorrectDetailsComments.Should().Be(ADDITIONAL_COMMENT);
        }
    }
}
