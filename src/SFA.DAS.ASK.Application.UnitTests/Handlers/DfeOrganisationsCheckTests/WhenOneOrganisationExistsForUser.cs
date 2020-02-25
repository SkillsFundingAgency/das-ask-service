using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck;
using SFA.DAS.ASK.Application.Services.DfeApi;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DfeOrganisationsCheckTests
{
    public class WhenOneOrganisationExistsForUser : DfeOrganisationsCheckTestBase
    {
        private Guid _dfeOrganisationId;

        [SetUp]
        public async Task Arrange()
        {
            _dfeOrganisationId = Guid.NewGuid();
            ApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(new List<DfeOrganisation>(){new DfeOrganisation(){Id = _dfeOrganisationId}});
            Result = await Handler.Handle(new DfeOrganisationsCheckRequest(Guid.NewGuid()), CancellationToken.None);
        }
        
        [Test]
        public void ThenReturnedStatusIsSingle()
        {
            Result.DfeOrganisationCheckResult.Should().Be(DfeOrganisationCheckResult.Single);
        }

        [Test]
        public void ThenReturnedIdIsSet()
        {
            Result.Id.Should().Be(_dfeOrganisationId);
        }
    }
}