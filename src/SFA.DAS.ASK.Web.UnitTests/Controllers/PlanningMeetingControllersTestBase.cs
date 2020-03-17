﻿using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers
{
    public class PlanningMeetingControllersTestBase : ControllersTestBase
    {
        protected Guid MY_ID = Guid.NewGuid();
        protected Guid DELIVERY_PARTNER_ID_1 = Guid.NewGuid();
        protected Guid DELIVERY_PARTNER_ID_2 = Guid.NewGuid();
        protected Guid DP_ORGANISATION_ID = Guid.NewGuid();
        protected Guid PLANNING_MEETING_ID = Guid.NewGuid();
        protected Guid ORGANISATION_CONTACT_ID = Guid.NewGuid();
        protected DateTime MEETING_TIME_AND_DATE = new DateTime(2020, 1, 1, 12, 0, 0);

        protected PlanningMeeting GetPlanningMeeting()
        {
            return new PlanningMeeting()
            {
                Id = PLANNING_MEETING_ID,
                SupportRequestId = SUPPORT_ID,
                DeliveryPartnerContactId = DELIVERY_PARTNER_ID_1,
                OrganisationContactId = ORGANISATION_CONTACT_ID,
                MeetingType = MeetingType.FaceToFace,
                MeetingTimeAndDate = MEETING_TIME_AND_DATE
            };
        }
        protected List<Data.Entities.DeliveryPartnerContact> GetDeliveryPartnerContacts()
        {
            return new List<Data.Entities.DeliveryPartnerContact>()
            {
                new Data.Entities.DeliveryPartnerContact()
                {
                    Id = DELIVERY_PARTNER_ID_1,
                    DeliveryPartnerOrganisationId = DP_ORGANISATION_ID,
                    FirstName = "Test1",
                    LastName = "Partner1",
                    Email = "Test1@DeliveryPartners.com",
                    PhoneNumber = "07111111111"
                },
                new Data.Entities.DeliveryPartnerContact()
                {
                    Id = DELIVERY_PARTNER_ID_2,
                    DeliveryPartnerOrganisationId = DP_ORGANISATION_ID,
                    FirstName = "Test2",
                    LastName = "Partner2",
                    Email = "Test2@DeliveryPartners.com",
                    PhoneNumber = "07222222222"
                }
            };
        }
    }
}