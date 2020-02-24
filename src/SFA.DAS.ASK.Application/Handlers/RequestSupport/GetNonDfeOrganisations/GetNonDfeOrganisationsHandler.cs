using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations
{
    public class GetNonDfeOrganisationsHandler : IRequestHandler<GetNonDfeOrganisationsRequest, IEnumerable<ReferenceDataSearchResult>>
    {
        private readonly IReferenceDataApiClient _referenceDataApiClient;
        private readonly ISessionService _sessionService;

        public GetNonDfeOrganisationsHandler(IReferenceDataApiClient referenceDataApiClient, ISessionService sessionService)
        {
            _referenceDataApiClient = referenceDataApiClient;
            _sessionService = sessionService;
        }

        public async Task<IEnumerable<ReferenceDataSearchResult>> Handle(GetNonDfeOrganisationsRequest request, CancellationToken cancellationToken)
        {
            List<ReferenceDataSearchResult> results;

            if (request.FromCache)
            {
               return JsonConvert.DeserializeObject<List<ReferenceDataSearchResult>>(_sessionService.Get($"Searchresults-{request.RequestId}"));
            }
            else
            {
                _sessionService.Set($"Searchstring-{request.RequestId}", request.SearchTerm);

                results = (await _referenceDataApiClient.Search(request.SearchTerm)).ToList();

                results.ForEach(o => o.Id = Guid.NewGuid());

                _sessionService.Set($"Searchresults-{request.RequestId}", JsonConvert.SerializeObject(results));

            }

            return results;
        }
    }
}
