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
    public class WhenMultipleOrganisationsExistsForUser : DfeOrganisationsCheckTestBase
    {
        [SetUp]
        public async Task Arrange()
        {
            ApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(new List<DfeOrganisation>(){new DfeOrganisation(){Id = Guid.NewGuid()},new DfeOrganisation(){Id = Guid.NewGuid()},new DfeOrganisation(){Id = Guid.NewGuid()}});
            Result = await Handler.Handle(new DfeOrganisationsCheckRequest(Guid.NewGuid()), CancellationToken.None);
        }
        
        [Test]
        public void ThenReturnedStatusIsMultiple()
        {
            Result.DfeOrganisationCheckResult.Should().Be(DfeOrganisationCheckResult.Multiple);
        }

        [Test]
        public void ThenReturnedIdIsSet()
        {
            Result.Id.Should().Be(default(Guid));
        }
    }
}