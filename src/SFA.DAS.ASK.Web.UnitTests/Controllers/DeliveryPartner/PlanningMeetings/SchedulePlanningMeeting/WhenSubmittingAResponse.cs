﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeeting;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.DeliveryPartner.PlanningMeetings.SchedulePlanningMeeting
{
    [TestFixture]
    public class WhenSubmittingAResponse : PlanningMeetingControllersTestBase
    {
        private SchedulePlanningMeetingController sut;

        [SetUp]
        public void Arrage()
        {
            Mediator.Send(Arg.Any<GetPlanningMeetingRequest>()).Returns(GetPlanningMeeting());
            Mediator.Send(Arg.Any<GetOrganisationContactsRequest>()).Returns(GetOrganisationContacts());
            Mediator.Send(Arg.Any<GetSupportRequest>()).Returns(GetSupportRequest());

            sut = new SchedulePlanningMeetingController(Mediator, SessionService);
        }

        [Test]
        public async Task AndTheModelIsValid_ThenTheUpdateHandlerisCalled_AndTheCorrectModelIsReturned()
        {
            var result = await sut.Index(SUPPORT_ID, GetSchedulePlanningMeetingViewModel());

            await Mediator.Received(1).Send(Arg.Any<UpdatePlanningMeetingCommand>());

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("PlanningContact");
        }

        private SchedulePlanningMeetingViewModel GetSchedulePlanningMeetingViewModel()
        {
            return new SchedulePlanningMeetingViewModel()
            {
                Day = 1,
                Month = 1,
                Year = DateTime.Now.AddYears(1).Year,
                Hours = 12,
                Minutes = 0,
                Type = Data.Entities.MeetingType.FaceToFace,

            };
        }
        private SchedulePlanningMeetingViewModel GetSchedulePlanningMeetingViewModelWithInvalidDate()
        {
            return new SchedulePlanningMeetingViewModel()
            {
                Day = 31,
                Month = 2,
                Year = 2020,
                Hours = 12,
                Minutes = 0,
                Type = Data.Entities.MeetingType.FaceToFace,
               
              
            };
        }
        private SchedulePlanningMeetingViewModel GetSchedulePlanningMeetingViewModelInThePast()
        {
            return new SchedulePlanningMeetingViewModel()
            {
                Day = 1,
                Month = 1,
                Year = DateTime.Now.AddYears(-1).Year,
                Hours = 12,
                Minutes = 0,
                Type = Data.Entities.MeetingType.FaceToFace,

            };
        }
    }
}
