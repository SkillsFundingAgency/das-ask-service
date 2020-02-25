using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.GetNonDfeOrganisations
{
    public class WhenNavigatingBackToTheSearchResultsPage : GetNonDfeOrganisationsTestBase
    {
        [Test]
        public async Task ThenSearchResultsShouldBeRetrievedFromTheCache()
        {
            var result = (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid(), true), default(CancellationToken))).ToList();

            result[0].Name.Should().Be("Test School");
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenTheGetNonDfeOrganisationRequestIsNeverCalled()
        {
            (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid(), true), default(CancellationToken))).ToList();

            referenceDataApi.DidNotReceive().Search(Arg.Is("Test School"));
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenSaveToCacheIsNever()
        {
            (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid(), true), default(CancellationToken))).ToList();

            sessionService.DidNotReceive().Set(Arg.Any<string>(), Arg.Any<string>());
        }
    }

}
