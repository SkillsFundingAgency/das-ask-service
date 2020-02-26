using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation;
using SFA.DAS.ASK.Application.Services.DfeApi;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.AddDfeSignInOrganisationTests
{
    public class WhenAddDfeSignInOrganisationWithNoAddressHandled : AddDfeSignInOrganisationTestBase
    {
        [Test]
        public async Task ThenTheTempSupportRequestAddressFieldsAreUpdated()
        {
            DfeSignInApiClient.GetOrganisations(DfeSignInId).Returns(new List<DfeOrganisation>{new DfeOrganisation
            {
                Id = SelectedOrganisationId,
                Address = null,
                Telephone = "029292929",
                Name = "Org1",
                Urn = "121212"
            }});
            
            await Handler.Handle(new AddDfESignInOrganisationCommand(TempSupportRequestId, SelectedOrganisationId), CancellationToken.None);

            var updatedTempSupportRequest = await DbContext.TempSupportRequests.SingleAsync(tsr => tsr.Id == TempSupportRequestId);

            updatedTempSupportRequest.BuildingAndStreet1.Should().Be("");
            updatedTempSupportRequest.BuildingAndStreet2.Should().Be("");
            updatedTempSupportRequest.TownOrCity.Should().Be("");
            updatedTempSupportRequest.County.Should().Be("");
            updatedTempSupportRequest.Postcode.Should().Be("");
        }
    }
}