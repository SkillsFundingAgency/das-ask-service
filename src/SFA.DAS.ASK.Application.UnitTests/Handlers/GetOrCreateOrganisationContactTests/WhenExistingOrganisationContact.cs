using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisationContact;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.GetOrCreateOrganisationContactTests
{
    [TestFixture]
    public class WhenExistingOrganisationContact : GetOrCreateOrganisationContactTestBase
    {
        
        private Guid _organisationId;
        private Guid _existingContactId;

        [SetUp]
        public async Task SetUp()
        {
            _organisationId = Guid.NewGuid();
            _existingContactId = Guid.NewGuid();
            await Context.OrganisationContacts.AddRangeAsync(new List<OrganisationContact>()
            {
                new OrganisationContact(){Id = _existingContactId, OrganisationId = _organisationId, Email = "email@domain.com"},
                new OrganisationContact(){Id = Guid.NewGuid(), OrganisationId = _organisationId, Email = "james@domain.com"}
            });
            await Context.SaveChangesAsync();
        }

        [Test]
        public async Task ThenExistingContactIsReturned()
        {
            var tempSupportRequest = new TempSupportRequest()
            {
                Email = "email@domain.com"
            };

            var result = await Handler.Handle(new GetOrCreateOrganisationContactRequest(tempSupportRequest, _organisationId), new CancellationToken());

            result.Id.Should().Be(_existingContactId);

        }
        
        [Test]
        public async Task ThenANewContactIsNotSaved()
        {
            var tempSupportRequest = new TempSupportRequest()
            {
                Email = "email@domain.com"
            };

            await Handler.Handle(new GetOrCreateOrganisationContactRequest(tempSupportRequest, _organisationId), new CancellationToken());

            (await Context.OrganisationContacts.CountAsync()).Should().Be(2);
        }
    }
}