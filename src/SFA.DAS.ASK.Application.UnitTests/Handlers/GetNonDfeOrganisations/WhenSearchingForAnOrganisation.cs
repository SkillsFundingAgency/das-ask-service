using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Services.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.GetNonDfeOrganisations
{
    [TestFixture]
    public class WhenSearchingForAnOrganisation : GetNonDfeOrganisationsTestBase
    {
        private Guid requestId = Guid.Parse("63be476e-0593-40c5-9b8d-8f0358a4d195");

        [Test]
        public async Task ThenResultsShouldBeFound()
        {
            var result = (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid(), false), default(CancellationToken))).ToList();

            result.Count.Should().Be(1);
            result[0].Name.Should().Be("Test School Search Result");
        }

        public async Task ThenAGuidShouldBeAdded()
        {
            var result = (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid(), false), default(CancellationToken))).ToList();

            result[0].Id.HasValue.Should().Be(true);
        }

        [Test]
        public async Task ThenSearchResultsAreSavedToCache()
        {
            var result = (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", requestId, false), default(CancellationToken))).ToList();

            sessionService.Received().Set(Arg.Is($"Searchresults-{requestId}"), Arg.Any<string>());
           
        }

        [Test]
        public async Task ThenSearchTermIsSavedToCache()
        {
            var result = (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", requestId, false), default(CancellationToken))).ToList();

            sessionService.Received().Set(Arg.Is<string>($"Searchstring-{requestId}"), Arg.Any<string>());

        }

    }
}
