using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations;
using SFA.DAS.ASK.Application.Services.ReferenceData;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetNonDfeOrganisations
{
    public class WhenNavigatingBackToTheSearchResultsPage : GetNonDfeOrganisationsTestBase
    {
        [Test]
        public async Task ThenSearchResultsShouldBeRetrievedFromTheCache()
        {
            SessionService.Get(Arg.Any<string>()).Returns("Test School");
            SessionService.Get<List<ReferenceDataSearchResult>>(Arg.Any<string>()).Returns(GetCachedSearchResults());

            var result = (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid()), default(CancellationToken))).ToList();

            result[0].Name.Should().Be("Test School");
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenTheGetNonDfeOrganisationRequestIsNeverCalled()
        {
            SessionService.Get(Arg.Any<string>()).Returns("Test School");
            SessionService.Get<List<ReferenceDataSearchResult>>(Arg.Any<string>()).Returns(GetCachedSearchResults());

            await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid()), default(CancellationToken));

            await ReferenceDataApi.DidNotReceive().Search(Arg.Is("Test School"));
        }

        [Test]
        public async Task FromTheCheckYourAnswersPage_ThenSaveToCacheIsNever()
        {
            SessionService.Get(Arg.Any<string>()).Returns("Test School");
            SessionService.Get<List<ReferenceDataSearchResult>>(Arg.Any<string>()).Returns(GetCachedSearchResults());

            (await Handler.Handle(new GetNonDfeOrganisationsRequest("Test School", Guid.NewGuid()), default(CancellationToken))).ToList();

            SessionService.DidNotReceive().Set(Arg.Any<string>(), Arg.Any<string>());
        }
    }

}
