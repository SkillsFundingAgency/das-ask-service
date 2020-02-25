using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.CancelSupportRequestTests
{
    [TestFixture]
    public class WhenCancelSupportRequestIsHandled
    {
        [Test]
        public async Task ThenCorrectSupportRequestIsCancelled()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var cancelledTempSupportRequestId = Guid.NewGuid();
            await dbContext.TempSupportRequests.AddRangeAsync(new List<TempSupportRequest>()
            {
                new TempSupportRequest(){Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
                new TempSupportRequest(){Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
                new TempSupportRequest(){Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
                new TempSupportRequest(){Id = cancelledTempSupportRequestId, Status = TempSupportRequestStatus.Active},
                new TempSupportRequest(){Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
            });
            await dbContext.SaveChangesAsync();
            
            var handler = new CancelSupportRequestHandler(dbContext);

            await handler.Handle(new CancelSupportRequestCommand(cancelledTempSupportRequestId, ""), CancellationToken.None);

            dbContext.TempSupportRequests.Count(tsr => tsr.Status == TempSupportRequestStatus.Active).Should().Be(4);
            dbContext.TempSupportRequests.Count(tsr => tsr.Status == TempSupportRequestStatus.Cancelled).Should().Be(1);
            var cancelledTempSupportRequest = dbContext.TempSupportRequests.Single(tsr => tsr.Status == TempSupportRequestStatus.Cancelled);
            
            cancelledTempSupportRequest.Id.Should().Be(cancelledTempSupportRequestId);
        }
    }
}