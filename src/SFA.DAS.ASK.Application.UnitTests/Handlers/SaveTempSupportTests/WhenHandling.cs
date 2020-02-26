using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.SaveTempSupportTests
{
    [TestFixture]
    public class WhenHandling
    {
        [Test]
        public async Task ThenSaveChangesIsCalled()
        {
            var context = Substitute.For<AskContext>();

            var handler = new SaveTempSupportHandler(context);
            await handler.Handle(new SaveTempSupportRequest(), CancellationToken.None);
            await context.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}