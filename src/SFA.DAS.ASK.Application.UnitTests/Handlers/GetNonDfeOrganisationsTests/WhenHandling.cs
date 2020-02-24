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
            var handler = new GetNonDfeOrganisationsHandler(Substitute.For<IReferenceDataApiClient>());
        }
        
    }
}