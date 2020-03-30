using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeting;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeting
{
    [TestFixture]
    public class WhenUpdatePlanningMeetingIsHandled
    {
        [Test]
        public async Task ThenSaveChangesIsCalled()
        {
            var askContext = Substitute.For<AskContext>();
            var handler = new UpdatePlanningMeetingHandler(askContext);
            await handler.Handle(new UpdatePlanningMeetingCommand(), CancellationToken.None);
            await askContext.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
