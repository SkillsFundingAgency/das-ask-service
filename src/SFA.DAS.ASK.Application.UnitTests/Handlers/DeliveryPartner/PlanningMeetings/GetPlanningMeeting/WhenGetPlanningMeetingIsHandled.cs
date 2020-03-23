using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.PlanningMeetings.GetPlanningMeetings
{
    [TestFixture]
    public class WhenGetPlanningMeetingIsHandled
    {
        private Guid PLANNING_MEETING_ID_1 = Guid.NewGuid();
        private Guid PLANNING_MEETING_ID_2 = Guid.NewGuid();

        private Guid SUPPORT_ID_1 = Guid.NewGuid();
        private Guid SUPPORT_ID_2 = Guid.NewGuid();

        [Test]
        public async Task ThenTheCorrectPlanningMeetingIsReturned()
        {
            var dbContext = ContextHelper.GetInMemoryContext();

            await dbContext.PlanningMeetings.AddRangeAsync(new List<PlanningMeeting>()
            {
                new PlanningMeeting()
                {
                    Id = PLANNING_MEETING_ID_1,
                    SupportRequestId = SUPPORT_ID_1
                },
                new PlanningMeeting()
                {
                    Id = PLANNING_MEETING_ID_2,
                    SupportRequestId = SUPPORT_ID_2
                },
            });

            await dbContext.SaveChangesAsync();

            var handler = new GetPlanningMeetingHandler(dbContext);

            var meeting = await handler.Handle(new GetPlanningMeetingRequest(SUPPORT_ID_2), CancellationToken.None);

            meeting.Id.Should().Be(PLANNING_MEETING_ID_2);
        }
    }
}
