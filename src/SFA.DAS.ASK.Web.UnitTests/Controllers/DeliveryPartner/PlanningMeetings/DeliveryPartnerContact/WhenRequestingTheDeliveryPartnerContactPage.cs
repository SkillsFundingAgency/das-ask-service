using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts;
using SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.ASK.Data.Entities;

using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.DeliveryPartner.PlanningMeetings.DeliveryPartnerContact
{
    [TestFixture]
    public class WhenRequestingTheDeliveryPartnerContactPage : PlanningMeetingControllersTestBase
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
        public async Task ThenTheViewIsReturnedWithTheCorrectModel()
        {

           
            var result = await sut.Index(SUPPORT_ID, false);

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/DeliveryPartner/PlanningMeetings/DeliveryPartnerContact.cshtml");

            var model = result.As<ViewResult>().Model.As<DeliveryPartnerContactViewModel>();
            model.DeliveryPartnerContacts.Count.Should().Be(2);
            model.SupportId.Should().Be(SUPPORT_ID);

        }

        [Test]
        public async Task ThenTheSelectedDeliveryPartnerContactIsPreSelected()
        {
            Mediator.Send(Arg.Any<GetDeliveryPartnerContactsRequest>()).Returns(GetDeliveryPartnerContacts());
            Mediator.Send(Arg.Any<GetPlanningMeetingRequest>()).Returns(GetPlanningMeeting());

            var result = await sut.Index(SUPPORT_ID, false);

            var model = result.As<ViewResult>().Model.As<DeliveryPartnerContactViewModel>();
            model.SelectedDeliveryPartnerContactId.Should().Be(DELIVERY_PARTNER_ID_1);
        
        }

    }
}
