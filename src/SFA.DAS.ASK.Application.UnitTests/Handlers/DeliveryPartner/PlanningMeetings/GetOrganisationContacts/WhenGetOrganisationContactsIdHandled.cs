using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts
{

    [TestFixture]
    public class WhenGetOrganisationContactsIdHandled
    {
        private Guid ORGANISATION_ID = Guid.NewGuid();

        [Test]
        public async Task ThenTheCorrectOrganisationContactsAreReturned()
        {
            var dbContext = ContextHelper.GetInMemoryContext();

            await dbContext.OrganisationContacts.AddRangeAsync(GetListOfOrganisationContacts());

            await dbContext.SaveChangesAsync();

            var handler = new GetOrganisationContactsHandler(dbContext);

            var contacts = await handler.Handle(new GetOrganisationContactsRequest(ORGANISATION_ID), CancellationToken.None);

            contacts.Count.Should().Be(2);
        }

        private List<OrganisationContact> GetListOfOrganisationContacts()
        {

            return new List<OrganisationContact>()
            {
                new OrganisationContact()
                {
                    Id = Guid.NewGuid(),
                    OrganisationId = ORGANISATION_ID
                },
                new OrganisationContact()
                {
                    Id = Guid.NewGuid(),
                    OrganisationId = ORGANISATION_ID
                },
                new OrganisationContact()
                {
                    Id = Guid.NewGuid(),
                    OrganisationId = Guid.NewGuid()
                }
            };
        }
    }
}
