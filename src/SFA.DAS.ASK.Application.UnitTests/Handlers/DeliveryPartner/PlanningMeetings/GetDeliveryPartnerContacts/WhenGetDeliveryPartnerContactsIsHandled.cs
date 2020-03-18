using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts
{
    [TestFixture]
    public class WhenGetDeliveryPartnerContactsIsHandled
    {
        private Guid DELIVERY_PARTNER_ORGANISATION_ID = Guid.NewGuid();

        [Test]
        public async Task ThenTheCorrectDeliveryPartnersAreReturned()
        {
            var dbContext = ContextHelper.GetInMemoryContext();

            await dbContext.DeliveryPartnerContacts.AddRangeAsync(GetListOfDeliveryPartnerContacts());

            await dbContext.SaveChangesAsync();

            var handler = new GetDeliveryPartnerContactsHandler(dbContext);

            var contacts = await handler.Handle(new GetDeliveryPartnerContactsRequest(DELIVERY_PARTNER_ORGANISATION_ID), CancellationToken.None);

            contacts.Count.Should().Be(2);
        }

        private List<DeliveryPartnerContact> GetListOfDeliveryPartnerContacts()
        {

            return new List<DeliveryPartnerContact>()
            {
                new DeliveryPartnerContact()
                {
                    Id = Guid.NewGuid(),
                    DeliveryPartnerOrganisationId = DELIVERY_PARTNER_ORGANISATION_ID
                },
                new DeliveryPartnerContact()
                {
                    Id = Guid.NewGuid(),
                    DeliveryPartnerOrganisationId = DELIVERY_PARTNER_ORGANISATION_ID
                },
                new DeliveryPartnerContact()
                {
                    Id = Guid.NewGuid(),
                    DeliveryPartnerOrganisationId = Guid.NewGuid()
                }
            };
        }
    }
}
