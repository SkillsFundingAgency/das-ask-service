using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers
{
    public class PlanningMeetingControllersTestBase : ControllersTestBase
    {
        protected Guid MyId = Guid.NewGuid();
        protected Guid DeliveryPartnerId1 = Guid.NewGuid();
        protected Guid DeliveryPartnerId2 = Guid.NewGuid();
        protected Guid DpOrganisationId = Guid.NewGuid();
        protected Guid PlanningMeetingId = Guid.NewGuid();
        protected Guid OrganisationId = Guid.NewGuid();
        protected Guid OrganisationContactId1 = Guid.NewGuid();
        protected Guid OrganisationContactId2 = Guid.NewGuid();
        protected DateTime MeetingTimeAndDate = new DateTime(2020, 1, 1, 12, 0, 0);

        protected PlanningMeeting GetPlanningMeeting()
        {
            return new PlanningMeeting()
            {
                Id = PlanningMeetingId,
                SupportRequestId = SUPPORT_ID,
                DeliveryPartnerContactId = DeliveryPartnerId1,
                OrganisationContactId = OrganisationContactId1,
                MeetingType = MeetingType.FaceToFace,
                MeetingTimeAndDate = MeetingTimeAndDate
            };
        }
        protected List<Data.Entities.DeliveryPartnerContact> GetDeliveryPartnerContacts()
        {
            return new List<Data.Entities.DeliveryPartnerContact>()
            {
                new Data.Entities.DeliveryPartnerContact()
                {
                    Id = DeliveryPartnerId1,
                    DeliveryPartnerId = DpOrganisationId,
                    FullName = "Test1 Partner1",
                    Email = "Test1@DeliveryPartners.com",
                    PhoneNumber = "07111111111"
                },
                new Data.Entities.DeliveryPartnerContact()
                {
                    Id = DeliveryPartnerId2,
                    DeliveryPartnerId = DpOrganisationId,
                    FullName = "Test2 Partner2",
                    Email = "Test2@DeliveryPartners.com",
                    PhoneNumber = "07222222222"
                }
            };
        }

        protected List<OrganisationContact> GetOrganisationContacts()
        {
            return new List<OrganisationContact>()
            {
                new OrganisationContact()
                {
                    FirstName = "Test1",
                    LastName = "Contact1",
                    Email = "Test1@organisation.com",
                    PhoneNumber = "07222222222",
                    Id = OrganisationContactId1,
                    OrganisationId = OrganisationId
                },
                new OrganisationContact()
                {
                    FirstName = "Test2",
                    LastName = "Contact2",
                    Email = "Test2@organisation.com",
                    PhoneNumber = "07222222222",
                    Id = OrganisationContactId2,
                    OrganisationId = OrganisationId
                }
            };
        }
        protected SupportRequest GetSupportRequest()
        {
            return new SupportRequest()
            {
                OrganisationId = OrganisationId,
                Organisation = new Organisation() { OrganisationName = "Test Organisation" }
            };
        }
    }
}
