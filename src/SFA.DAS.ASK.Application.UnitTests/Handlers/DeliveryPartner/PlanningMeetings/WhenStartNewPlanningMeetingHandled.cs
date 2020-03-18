using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.StartPlanningMeeting;
using SFA.DAS.ASK.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.PlanningMeetings
{
    [TestFixture]
    public class WhenStartNewPlanningMeetingHandled 
    {
        private Guid REQUEST_ID = Guid.NewGuid();

        [Test]
        public async Task ThenANewPlanningMeetingIsCreated()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AskContext(dbContextOptions);

            var handler = new StartPlanningMeetingHandler(context);

            var result = await handler.Handle(new StartPlanningMeetingCommand(REQUEST_ID), CancellationToken.None);

            (await context.PlanningMeetings.CountAsync()).Should().Be(1);
        }
    }
}
