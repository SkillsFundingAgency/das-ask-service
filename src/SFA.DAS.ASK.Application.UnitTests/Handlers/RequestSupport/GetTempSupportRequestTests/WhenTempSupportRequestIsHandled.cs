using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetTempSupportRequestTests
{
    [TestFixture]
    public class WhenTempSupportRequestIsHandled
    {
        [Test]
        public async Task ThenTheCorrectTempSupportRequestIsReturned()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var requiredSupportRequestId = Guid.NewGuid();
            var requestedSupportRequest = new TempSupportRequest {Id = requiredSupportRequestId, OrganisationName = "Org 2"};
            
            await dbContext.TempSupportRequests.AddRangeAsync(new List<TempSupportRequest>
            {
                new TempSupportRequest {Id = Guid.NewGuid(), OrganisationName = "Org 1"},
                requestedSupportRequest,
                new TempSupportRequest {Id = Guid.NewGuid(), OrganisationName = "Org 3"},
            });
            await dbContext.SaveChangesAsync();
            
            var handler = new GetTempSupportRequestHandler(dbContext);

            var supportRequest = await handler.Handle(new GetTempSupportRequest(requiredSupportRequestId), CancellationToken.None);
            
            supportRequest.Should().BeEquivalentTo(requestedSupportRequest);

        }
    }
}