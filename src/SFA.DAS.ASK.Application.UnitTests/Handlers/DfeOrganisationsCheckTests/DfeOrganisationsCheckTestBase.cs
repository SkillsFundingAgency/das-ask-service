using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck;
using SFA.DAS.ASK.Application.Services.DfeApi;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DfeOrganisationsCheckTests
{
    [TestFixture]
    public class DfeOrganisationsCheckTestBase
    {
        protected DfeOrganisationsCheckHandler Handler;
        protected DfeOrganisationsCheckResponse Result;
        protected IDfeSignInApiClient ApiClient;

        [SetUp]
        public void SetUp()
        {
            ApiClient = Substitute.For<IDfeSignInApiClient>();
            Handler = new DfeOrganisationsCheckHandler(ApiClient);
        }
    }
}