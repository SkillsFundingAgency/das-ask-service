using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.CheckOrganisationLocation;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.CheckOrganisationLocationTests
{
    [TestFixture]
    public class WhenEnglishOrganisationLocationChecked
    {
        private CheckOrganisationLocationHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            _handler = new CheckOrganisationLocationHandler(dbContext);
        }
        
        [Test]
        public async Task ThenTrueIsReturnedForEnglishPostcode()
        {
            
            var result = await _handler.Handle(new CheckOrganisationLocationRequest("WS12 4YQ"), CancellationToken.None);
            result.Should().BeTrue();
        }
        
        [Test]
        public async Task ThenFalseIsReturnedForNoneEnglishPostcode()
        {
            var result = await _handler.Handle(new CheckOrganisationLocationRequest("EH2 2BY"), CancellationToken.None);
            result.Should().BeFalse();
        }
    }
}
