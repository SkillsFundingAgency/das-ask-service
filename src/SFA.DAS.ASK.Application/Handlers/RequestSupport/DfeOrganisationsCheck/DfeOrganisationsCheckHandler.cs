using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.DfeApi;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck
{
    public class DfeOrganisationsCheckHandler : IRequestHandler<DfeOrganisationsCheckRequest, DfeOrganisationsCheckResponse>
    {
        private readonly IDfeSignInApiClient _dfeApiClient;

        public DfeOrganisationsCheckHandler(IDfeSignInApiClient dfeApiClient)
        {
            _dfeApiClient = dfeApiClient;
        }
        
        public async Task<DfeOrganisationsCheckResponse> Handle(DfeOrganisationsCheckRequest request, CancellationToken cancellationToken)
        {
            var dfeOrganisations = _dfeApiClient.GetOrganisations(request.DfeSignInId);
            if (dfeOrganisations is null || !dfeOrganisations.Any())
            {
                return new DfeOrganisationsCheckResponse()
                {
                    DfeOrganisationsStatus = DfeOrganisationsStatus.None
                };
            }

            if (dfeOrganisations.Count > 1)
            {
                return new DfeOrganisationsCheckResponse()
                {
                    DfeOrganisationsStatus = DfeOrganisationsStatus.Multiple
                };
            }

            return new DfeOrganisationsCheckResponse()
            {
                DfeOrganisationsStatus = DfeOrganisationsStatus.Single,
                Urn = dfeOrganisations.Single().Urn
            };
        }
    }
}