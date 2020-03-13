using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitComplete;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitFeedbackComplete;
using SFA.DAS.ASK.Application.Services.Email;

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

            await dbContext.VisitFeedback.AddAsync(new VisitFeedback()
            {
                Id = requestFeedbackId, 
                Status = FeedbackStatus.NotStarted,
                Visit = new Visit()
                {
                    OrganisationContact = new OrganisationContact(){Email = "contact@org.com", FirstName = "Fred"},
                    SupportRequest = new SupportRequest()
                    {
                        Organisation = new Organisation()
                        {
                            OrganisationName = "Org1"
                        }
                    }
                }
            });
            await dbContext.SaveChangesAsync();

            var handler = new SetVisitFeedbackCompleteHandler(dbContext, Substitute.For<IEmailService>());
            await handler.Handle(new SetVisitFeedbackCompleteCommand(requestFeedbackId, FeedbackStatus.Complete), CancellationToken.None);

            (await dbContext.VisitFeedback.Where(visit => visit.Id == requestFeedbackId).SingleAsync()).Status.Should().Be(FeedbackStatus.Complete); 
        }
        
        [Test]
        public async Task ThenEmailIsSent()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var requestFeedbackId = Guid.NewGuid();

            await dbContext.VisitFeedback.AddAsync(new VisitFeedback()
            {
                Id = requestFeedbackId, 
                Status = FeedbackStatus.NotStarted,
                Visit = new Visit()
                {
                    OrganisationContact = new OrganisationContact(){Email = "contact@org.com", FirstName = "Fred"},
                    SupportRequest = new SupportRequest()
                    {
                        Organisation = new Organisation()
                        {
                            OrganisationName = "Org1"
                        }
                    }
                }
            });
            await dbContext.SaveChangesAsync();

            var emailService = Substitute.For<IEmailService>();
            
            var handler = new SetVisitFeedbackCompleteHandler(dbContext, emailService);
            await handler.Handle(new SetVisitFeedbackCompleteCommand(requestFeedbackId, FeedbackStatus.Complete), CancellationToken.None);

            await emailService.Received().SendFeedbackSubmitted("contact@org.com", "Fred", "Org1");
        }
    }
}
