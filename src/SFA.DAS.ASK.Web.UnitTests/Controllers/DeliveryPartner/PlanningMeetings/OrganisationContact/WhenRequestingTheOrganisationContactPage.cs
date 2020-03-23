using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.DeliveryPartner.PlanningMeetings.OrganisationContact
{
    [TestFixture]
    public class WhenRequestingTheOrganisationContactPage : PlanningMeetingControllersTestBase
    {
        private PlanningContactController sut;

        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetPlanningMeetingRequest>()).Returns(GetPlanningMeeting());
            Mediator.Send(Arg.Any<GetOrganisationContactsRequest>()).Returns(GetOrganisationContacts());
            Mediator.Send(Arg.Any<GetSupportRequest>()).Returns(GetSupportRequest());

            sut = new PlanningContactController(Mediator, SessionService);
        }

        [Test]
        public async Task ThenTheCorrectViewIsReturned()
        {
            
            var result = await sut.Index(SUPPORT_ID, false);

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/DeliveryPartner/PlanningMeetings/PlanningContact.cshtml");
        }

        [Test]
        public async Task ThenContactsAreSavedToSessionStorage()
        {
            var result = await sut.Index(SUPPORT_ID, false);

            SessionService.Received(1).Set<List<Data.Entities.OrganisationContact>>(Arg.Any<string>(),Arg.Any<List<Data.Entities.OrganisationContact>>() );
        }
    }
}
