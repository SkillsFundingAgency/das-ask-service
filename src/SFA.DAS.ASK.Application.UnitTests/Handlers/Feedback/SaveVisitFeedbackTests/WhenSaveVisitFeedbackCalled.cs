using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.SaveVisitFeedback;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.Feedback.SaveVisitFeedbackTests
{
    [TestFixture]
    public class WhenSaveVisitFeedbackCalled
    {
        [Test]
        public async Task ThenFeedbackIsSaved()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var requestedFeedbackId = Guid.NewGuid();
            await dbContext.VisitFeedback.AddRangeAsync(new List<VisitFeedback>
            {
                new VisitFeedback{Status = FeedbackStatus.NotStarted, Id = Guid.NewGuid()},
                new VisitFeedback{Status = FeedbackStatus.NotStarted, Id = requestedFeedbackId, 
                    Visit = new Visit() 
                    {
                        Activities = new List<VisitActivity>(), 
                        SupportRequest = new SupportRequest()
                        {
                            Organisation = new Organisation(),
                            OrganisationContact = new OrganisationContact()
                        }
                    }},
            });
            await dbContext.SaveChangesAsync();
            
            var handler = new SaveVisitFeedbackHandler(dbContext);

            await handler.Handle(new SaveVisitFeedbackRequest(requestedFeedbackId, new FeedbackAnswers{ ActivitiesDelivered = FeedbackRating.Poor, AskDeliveryPartnerWhoVisited = FeedbackRating.Good}), CancellationToken.None);

            var savedFeedback = await dbContext.VisitFeedback.SingleAsync(f => f.Id == requestedFeedbackId);
            savedFeedback.FeedbackAnswers.InformationAndCommunicationBeforeVisit.Should().BeNull();
            savedFeedback.FeedbackAnswers.ActivitiesDelivered.Should().Be(FeedbackRating.Poor);
            savedFeedback.FeedbackAnswers.AskDeliveryPartnerWhoVisited.Should().Be(FeedbackRating.Good);
        }
    }
}