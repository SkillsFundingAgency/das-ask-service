using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation;
using SFA.DAS.ASK.Application.Services.DfeApi;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.AddDfeSignInOrganisationTests
{
    public class WhenAddDfeSignInOrganisationHandled : AddDfeSignInOrganisationTestBase
    {
        [Test]
        public async Task ThenTheTempSupportRequestIsUpdated()
        {
            DfeSignInApiClient.GetOrganisations(DfeSignInId).Returns(new List<DfeOrganisation>{new DfeOrganisation
            {
                Id = SelectedOrganisationId,
                Address = "School Road, Village, Town, Staffs, WS12 4YQ",
                Telephone = "029292929",
                Name = "Org1",
                Urn = "121212"
            }});
            
            await Handler.Handle(new AddDfESignInOrganisationCommand(TempSupportRequestId, SelectedOrganisationId), CancellationToken.None);

            var updatedTempSupportRequest = await DbContext.TempSupportRequests.SingleAsync(tsr => tsr.Id == TempSupportRequestId);

            updatedTempSupportRequest.BuildingAndStreet1.Should().Be("School Road");
            updatedTempSupportRequest.BuildingAndStreet2.Should().Be("Village");
            updatedTempSupportRequest.TownOrCity.Should().Be("Town");
            updatedTempSupportRequest.County.Should().Be("Staffs");
            updatedTempSupportRequest.Postcode.Should().Be("WS12 4YQ");
            updatedTempSupportRequest.PhoneNumber.Should().Be("029292929");
            updatedTempSupportRequest.OrganisationName.Should().Be("Org1");
            updatedTempSupportRequest.ReferenceId.Should().Be("121212");
            updatedTempSupportRequest.SelectedDfeSignInOrganisationId.Should().Be(SelectedOrganisationId);
        }
    }
}