using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Services.ReferenceData;
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
            SessionService.Get<List<ReferenceDataSearchResult>>(Arg.Any<string>()).Returns(GetCachedSearchResults());

            var result = (await Handler.Handle(new GetOrganisationsRequest("Test School", Guid.NewGuid()), default(CancellationToken))).ToList();

            result[0].Name.Should().Be("Test School");
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenTheGetNonDfeOrganisationRequestIsNeverCalled()
        {
            SessionService.Get<List<ReferenceDataSearchResult>>(Arg.Any<string>()).Returns(GetCachedSearchResults());

            (await Handler.Handle(new GetOrganisationsRequest("Test School", Guid.NewGuid()), default(CancellationToken))).ToList();

            ReferenceDataApi.DidNotReceive().Search(Arg.Is("Test School"));
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenSaveToCacheIsNever()
        {
            SessionService.Get<List<ReferenceDataSearchResult>>(Arg.Any<string>()).Returns(GetCachedSearchResults());

            (await Handler.Handle(new GetOrganisationsRequest("Test School", Guid.NewGuid()), default(CancellationToken))).ToList();

            SessionService.DidNotReceive().Set(Arg.Any<string>(), Arg.Any<string>());
        }
    }

}
