using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContact;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContact
{
    [TestFixture]
    public class WhenGetDeliveryPartnerContactHandled
    {
        private Guid DELIVERY_PARTNER_CONTACT_ID_1 = Guid.NewGuid();
        private Guid DELIVERY_PARTNER_CONTACT_ID_2 = Guid.NewGuid();

        [Test]
        public async Task ThenTheCorrectDeliveryPartnerContactIsReturned()
        {
            var dbContext = ContextHelper.GetInMemoryContext();

            await dbContext.DeliveryPartnerContacts.AddRangeAsync(new List<DeliveryPartnerContact>()
            {
                new DeliveryPartnerContact()
                {  
                    Id = DELIVERY_PARTNER_CONTACT_ID_1,
                    FullName = "Test1 DP1"
                },
                new DeliveryPartnerContact()
                {
                    Id = DELIVERY_PARTNER_CONTACT_ID_2,
                    FullName = "Test2 DP2"
                },
            });

            await dbContext.SaveChangesAsync();

            var handler = new GetDeliveryPartnerContactHandler(dbContext);

            var contact = await handler.Handle(new GetDeliveryPartnerContactRequest(DELIVERY_PARTNER_CONTACT_ID_1), CancellationToken.None);

            contact.FullName.Should().Be("Test1 DP1");

        }
    }
}
