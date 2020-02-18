using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.DfeApi;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations
{
    public class GetNonDfeOrganisationsHandler : IRequestHandler<GetNonDfeOrganisationsRequest, List<NonDfeOrganisation>>
    {
        private readonly INonDfeSignInApiClient _nonDfeApiClient;

        public GetNonDfeOrganisationsHandler(INonDfeSignInApiClient nonDfeApiClient)
        {
            _nonDfeApiClient = nonDfeApiClient;
        }
        public async Task<List<NonDfeOrganisation>> Handle(GetNonDfeOrganisationsRequest request, CancellationToken cancellationToken)
        {
            return _nonDfeApiClient.GetOrganisations();
        }
    }
}
