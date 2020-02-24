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

        public async Task<ReferenceDataSearchResult> Handle(GetSelectedOrganisationSearchResultRequest request, CancellationToken cancellationToken)
        {
            var cachedResults = JsonConvert.DeserializeObject<List<ReferenceDataSearchResult>>(_sessionService.Get($"Searchresults-{request.RequestId}"));

            return cachedResults.Where(r => r.Id == request.SelectedResult).FirstOrDefault();
        }
    }
}

       