using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.StartTempSupportRequestTests
{
    [TestFixture]
    public class WhenStartTempSupportRequestHandled
    {
        [Test]
        public async Task ThenANewTempSupportRequestIsSaved()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AskContext(dbContextOptions);

            var handler = new StartTempSupportRequestHandler(context);

            var result = await handler.Handle(new StartTempSupportRequestCommand(SupportRequestType.DfeSignIn), CancellationToken.None);

            (await context.TempSupportRequests.CountAsync()).Should().Be(1);

            var savedSupportRequest = await context.TempSupportRequests.SingleAsync();

            savedSupportRequest.SupportRequestType.Should().Be(SupportRequestType.DfeSignIn);

            result.RequestId.Should().Be(savedSupportRequest.Id);
        }
    }
}