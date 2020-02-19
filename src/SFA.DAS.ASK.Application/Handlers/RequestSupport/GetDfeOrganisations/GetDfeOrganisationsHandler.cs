using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.DfeApi;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetDfeOrganisations
{
    public class GetDfeOrganisationsHandler : IRequestHandler<GetDfeOrganisationsRequest, List<DfeOrganisation>>
    {
        private readonly IDfeSignInApiClient _dfeApiClient;

        public GetDfeOrganisationsHandler(IDfeSignInApiClient dfeApiClient)
        {
            _dfeApiClient = dfeApiClient;
        }
        public async Task<List<DfeOrganisation>> Handle(GetDfeOrganisationsRequest request, CancellationToken cancellationToken)
        {
            return await _dfeApiClient.GetOrganisations(request.DfeSignInId);
        }
    }
}