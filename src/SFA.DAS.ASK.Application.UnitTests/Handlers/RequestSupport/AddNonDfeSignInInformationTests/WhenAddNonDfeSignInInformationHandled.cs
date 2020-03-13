using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddNonDfeSignInInformation;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.AddNonDfeSignInInformationTests
{
    [TestFixture]
    public class WhenAddNonDfeSignInInformationHandled
    {
        [Test]
        public async Task ThenTempSupportRequestIsUpdatedWithOrganisationInformation()
        {
            var tempSupportRequestId = Guid.NewGuid();
            var dbContext = ContextHelper.GetInMemoryContext();
            await dbContext.TempSupportRequests.AddRangeAsync(new List<TempSupportRequest>
            {
                new TempSupportRequest(){Id = Guid.NewGuid()},
                new TempSupportRequest(){Id = tempSupportRequestId},
            });
            await dbContext.SaveChangesAsync();
            
            var handler = new AddNonDfESignInInformationHandler(dbContext);
            await handler.Handle(new AddNonDfESignInInformationCommand(new ReferenceDataSearchResult()
            {
                Address = new ReferenceDataAddress()
                {
                    Line1 = "AddressLine1",
                    Line2 = "AddressLine2",
                    Line3 = "AddressLine3",
                    Line4 = "AddressLine4",
                    Line5 = "AddressLine5",
                    Postcode = "AddressPostcode"
                }, 
                Name = "OrganisationName",
                Code = "123456"
            }, tempSupportRequestId), CancellationToken.None);

            dbContext.TempSupportRequests.Single(tsr => tsr.Id != tempSupportRequestId).OrganisationName.Should().BeNullOrEmpty();
            var updatedTempSupportRequest = dbContext.TempSupportRequests.Single(tsr => tsr.Id == tempSupportRequestId);
            
            updatedTempSupportRequest.OrganisationName.Should().Be("OrganisationName");
            updatedTempSupportRequest.ReferenceId.Should().Be("123456");
            
            updatedTempSupportRequest.BuildingAndStreet1.Should().Be("AddressLine1");
            updatedTempSupportRequest.BuildingAndStreet2.Should().Be("AddressLine2");
            updatedTempSupportRequest.TownOrCity.Should().Be("AddressLine3");
            updatedTempSupportRequest.County.Should().Be("AddressLine4");
            updatedTempSupportRequest.Postcode.Should().Be("AddressPostcode");
        }
    }
}