using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.ReferenceData;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations
{
    public class GetNonDfeOrganisationsHandler : IRequestHandler<GetNonDfeOrganisationsRequest, IEnumerable<ReferenceDataSearchResult>>
    {
        private readonly IReferenceDataApiClient _referenceDataApiClient;

        public GetNonDfeOrganisationsHandler(IReferenceDataApiClient referenceDataApiClient)
        {
            _referenceDataApiClient = referenceDataApiClient;
        }
        public async Task<IEnumerable<ReferenceDataSearchResult>> Handle(GetNonDfeOrganisationsRequest request, CancellationToken cancellationToken)
        {
            return await _referenceDataApiClient.Search(request.SearchTerm);
        }
    }
}
