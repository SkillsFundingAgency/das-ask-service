using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisation;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetOrCreateOrganisationTests
{
    public class WhenExistingOrganisation : GetOrCreateOrganisationTestBase
    {
        private Guid _existingOrganisationId;

        [SetUp]
        public async Task SetUp()
        {
            _existingOrganisationId = Guid.NewGuid();
            await Context.Organisations.AddRangeAsync(new List<Organisation>
            {
                new Organisation(){Id = _existingOrganisationId, ReferenceId = "REF1234"},
                new Organisation(){Id = Guid.NewGuid(), ReferenceId = "REF9999"}
            });
            await Context.SaveChangesAsync();
        }

        [Test]
        public async Task ThenExistingOrganisationIsReturned()
        {
            var result = await Handler.Handle(new GetOrCreateOrganisationRequest(new TempSupportRequest(){ReferenceId = "REF1234"}), new CancellationToken());
            result.Id.Should().Be(_existingOrganisationId);
        }

        [Test]
        public async Task ThenANewOrganisationIsNotSaved()
        {
            await Handler.Handle(new GetOrCreateOrganisationRequest(new TempSupportRequest(){ReferenceId = "REF1234"}), new CancellationToken());
            (await Context.Organisations.CountAsync()).Should().Be(2);
        }
    }
}