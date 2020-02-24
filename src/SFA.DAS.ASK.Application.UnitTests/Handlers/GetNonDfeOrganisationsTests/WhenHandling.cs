using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Services.ReferenceData;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.GetNonDfeOrganisationsTests
{
    [TestFixture]
    public class WhenHandling
    {
        [Test]
        public async Task ThenSearchIsPassedOnToApiClient()
        {
            var referenceDataApiClient = Substitute.For<IReferenceDataApiClient>();
            var handler = new GetNonDfeOrganisationsHandler(referenceDataApiClient);
            
            await handler.Handle(new GetNonDfeOrganisationsRequest("theSearchTerm"), CancellationToken.None);

            await referenceDataApiClient.Received(1).Search("theSearchTerm");
        }
    }
}