using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisationContact;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetOrCreateOrganisationContactTests
{
    [TestFixture]
    public class WhenNoExistingOrganisationContact : GetOrCreateOrganisationContactTestBase
    {
        [Test]
        public async Task ThenNewContactIsSaved()
        {
            var tempSupportRequest = new TempSupportRequest()
            {
                Email = "email@domain.com",
                FirstName = "Dave",
                LastName = "Smith",
                PhoneNumber = "080882088008"
            };

            var organisationId = Guid.NewGuid();
            var result = await Handler.Handle(new GetOrCreateOrganisationContactRequest(tempSupportRequest, organisationId), new CancellationToken());

            (await Context.OrganisationContacts.CountAsync()).Should().Be(1);
            var savedContact = await Context.OrganisationContacts.SingleAsync();
            savedContact.Email.Should().Be("email@domain.com");
            savedContact.FirstName.Should().Be("Dave");
            savedContact.LastName.Should().Be("Smith");
            savedContact.PhoneNumber.Should().Be("080882088008");
            savedContact.OrganisationId.Should().Be(organisationId);

            result.Should().Be(savedContact);
        }
    }
}