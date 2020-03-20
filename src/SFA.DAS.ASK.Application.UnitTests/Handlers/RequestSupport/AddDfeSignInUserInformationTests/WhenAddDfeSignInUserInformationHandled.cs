using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInUserInformation;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.AddDfeSignInUserInformationTests
{
    [TestFixture]
    public class WhenAddDfeSignInUserInformationHandled
    {
        [Test]
        public async Task ThenTempSupportRequestIsUpdatedWithDfeSignInUserInformation()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var tempSupportRequestId = Guid.NewGuid();
            await dbContext.TempSupportRequests.AddRangeAsync(new List<TempSupportRequest>
            {
                new TempSupportRequest(){Id = Guid.NewGuid()},
                new TempSupportRequest(){Id = tempSupportRequestId},
            });
            await dbContext.SaveChangesAsync();
            
            var handler = new AddDfeSignInUserInformationHandler(dbContext);

            var dfeSignInId = Guid.NewGuid();
            await handler.Handle(new AddDfeSignInUserInformationCommand("email@address.com", "firstname", "lastname", tempSupportRequestId, dfeSignInId), CancellationToken.None);
            
            dbContext.TempSupportRequests.Single(tsr => tsr.Id != tempSupportRequestId).Email.Should().BeNullOrEmpty();
            var updatedTempSupportRequest = dbContext.TempSupportRequests.Single(tsr => tsr.Id == tempSupportRequestId);
            
            updatedTempSupportRequest.Agree.Should().BeTrue();
            updatedTempSupportRequest.Email.Should().Be("email@address.com");
            updatedTempSupportRequest.FirstName.Should().Be("firstname");
            updatedTempSupportRequest.LastName.Should().Be("lastname");
            updatedTempSupportRequest.DfeSignInId.Should().Be(dfeSignInId);
        }
    }
}