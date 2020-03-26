using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeeting;
using SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.DeliveryPartner.PlanningMeetings.DeliveryPartnerContact
{
    [TestFixture]
    public class WhenSubmittingAResponse : PlanningMeetingControllersTestBase
    {
        private DeliveryPartnerContactController sut;

        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetDeliveryPartnerContactsRequest>()).Returns(GetDeliveryPartnerContacts());
            Mediator.Send(Arg.Any<GetPlanningMeetingRequest>()).Returns(GetPlanningMeeting());

            sut = new DeliveryPartnerContactController(Mediator, SessionService);
        }

        [Test]
        public async Task AndTheModelStateIsValid_ThenUpdatePlanningMeetingCommandIsCalled()
        {
            var result = sut.Submit(SUPPORT_ID, GetDeliveryPartnerContactViewModel());

            await Mediator.Received().Send(Arg.Any<UpdatePlanningMeetingCommand>());
        }
        [Test]
        public async Task AndTheModelStateIsValid_ThenTheUserIsRedirectedToTheCheckAnswersPage()
        {
            var result = await sut.Submit(SUPPORT_ID, GetDeliveryPartnerContactViewModel());

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("CheckAnswers");
        }

        [Test]
        public async Task AndTheModelStateIsInValid_ThenTheUserIsRedirectedToTheCheckAnswersPage()
        {
            var result = await sut.Submit(SUPPORT_ID, GetInvalidDeliveryPartnerContactViewModel());

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ControllerName.Should().Be("DeliveryPartnerContact");
        }

        private DeliveryPartnerContactViewModel GetDeliveryPartnerContactViewModel()
        {
            return new DeliveryPartnerContactViewModel()
            {
                DeliveryPartnerContacts = GetDeliveryPartnerContacts(),
                SelectedDeliveryPartnerContactId = DeliveryPartnerId1,
                SupportId = SUPPORT_ID,
                Edit = false
            };
        }
        private DeliveryPartnerContactViewModel GetInvalidDeliveryPartnerContactViewModel()
        {
            return new DeliveryPartnerContactViewModel()
            {
                DeliveryPartnerContacts = GetDeliveryPartnerContacts(),
                SelectedDeliveryPartnerContactId = Guid.Empty,
                SupportId = SUPPORT_ID,
                Edit = false
            };
        }
    }
}
