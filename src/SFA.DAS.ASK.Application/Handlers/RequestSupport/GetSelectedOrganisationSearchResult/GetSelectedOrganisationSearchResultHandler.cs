using MediatR;
using Newtonsoft.Json;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSelectedOrganisationSearchResult
{

    public class GetSelectedOrganisationSearchResultHandler : IRequestHandler<GetSelectedOrganisationSearchResultRequest,ReferenceDataSearchResult>
    {
        private readonly ISessionService _sessionService;
        public GetSelectedOrganisationSearchResultHandler(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

#pragma warning disable 1998
        public async Task<ReferenceDataSearchResult> Handle(GetSelectedOrganisationSearchResultRequest request, CancellationToken cancellationToken)
#pragma warning restore 1998
        {
            var cachedResults = _sessionService.Get<List<ReferenceDataSearchResult>>($"Searchresults-{request.RequestId}");

            return cachedResults.FirstOrDefault(r => r.Id == request.SelectedResult);
        }
    }
}

       