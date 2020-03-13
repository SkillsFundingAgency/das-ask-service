using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.CancelSupportRequestTests
{
    [TestFixture]
    public class WhenCancelSupportRequestIsHandled
    {
        private AskContext _dbContext;
        private Guid _cancelledTempSupportRequestId;
        private ISessionService _sessionService;

        [SetUp]
        public async Task SetUp()
        {
            _dbContext = ContextHelper.GetInMemoryContext();
            _cancelledTempSupportRequestId = Guid.NewGuid();
            await _dbContext.TempSupportRequests.AddRangeAsync(new List<TempSupportRequest>
            {
                new TempSupportRequest{Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
                new TempSupportRequest{Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
                new TempSupportRequest{Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
                new TempSupportRequest{Id = _cancelledTempSupportRequestId, Status = TempSupportRequestStatus.Active},
                new TempSupportRequest{Id = Guid.NewGuid(), Status = TempSupportRequestStatus.Active},
            });
            await _dbContext.SaveChangesAsync();

            _sessionService = Substitute.For<ISessionService>();
            
            var handler = new CancelSupportRequestHandler(_dbContext, _sessionService);

            await handler.Handle(new CancelSupportRequestCommand(_cancelledTempSupportRequestId), CancellationToken.None);
        }
        
        [Test]
        public void ThenCorrectSupportRequestIsCancelled()
        {
            _dbContext.TempSupportRequests.Count(tsr => tsr.Status == TempSupportRequestStatus.Active).Should().Be(4);
            _dbContext.TempSupportRequests.Count(tsr => tsr.Status == TempSupportRequestStatus.Cancelled).Should().Be(1);
            var cancelledTempSupportRequest = _dbContext.TempSupportRequests.Single(tsr => tsr.Status == TempSupportRequestStatus.Cancelled);
            
            cancelledTempSupportRequest.Id.Should().Be(_cancelledTempSupportRequestId);
        }

        [Test]
        public void ThenSessionIsClearedOut()
        {
            _sessionService.Received().Remove("HasSignIn");
            _sessionService.Received().Remove("TempSupportRequestId");
            _sessionService.Received().Remove($"Searchstring-{_cancelledTempSupportRequestId}");
            _sessionService.Received().Remove($"Searchresults-{_cancelledTempSupportRequestId}");
        }
    }
}