using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisation;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.GetOrCreateOrganisationTests
{
    public class WhenNoExistingOrganisation : GetOrCreateOrganisationTestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await Context.Organisations.AddRangeAsync(new List<Organisation>
            {
                new Organisation(){Id = Guid.NewGuid(), ReferenceId = "REF1234"},
                new Organisation(){Id = Guid.NewGuid(), ReferenceId = "REF9999"}
            });
            await Context.SaveChangesAsync();
        }

        [Test]
        public async Task ThenANewOrganisationIsSavedAndReturned()
        {
            var tempSupportRequest = new TempSupportRequest
            {
                Id = Guid.NewGuid(),
                ReferenceId = "REF8765",
                BuildingAndStreet1 = "BS1",
                BuildingAndStreet2 = "BS2",
                TownOrCity = "TC",
                County = "C",
                Postcode = "PC",
                OrganisationName = "OrgName"
            };
            var result = await Handler.Handle(new GetOrCreateOrganisationRequest(tempSupportRequest), new CancellationToken());

            (await Context.Organisations.CountAsync()).Should().Be(1);
            var savedOrganisation = await Context.Organisations.SingleAsync();
            savedOrganisation.ReferenceId.Should().Be("REF8765");
            savedOrganisation.BuildingAndStreet1.Should().Be("BS1");
            savedOrganisation.BuildingAndStreet2.Should().Be("BS2");
            savedOrganisation.TownOrCity.Should().Be("TC");
            savedOrganisation.County.Should().Be("C");
            savedOrganisation.Postcode.Should().Be("PC");
            savedOrganisation.OrganisationName.Should().Be("OrgName");
            
            result.Should().Be(savedOrganisation);
        }
    }
}