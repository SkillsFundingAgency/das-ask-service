using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.Services.DfeApi;

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
            var dfeOrganisations = await _dfeApiClient.GetOrganisations(request.DfeSignInId);
            if (dfeOrganisations is null || !dfeOrganisations.Any())
            {
                return new DfeOrganisationsCheckResponse()
                {
                    DfeOrganisationCheckResult = DfeOrganisationCheckResult.None
                };
            }

            if (dfeOrganisations.Count > 1)
            {
                return new DfeOrganisationsCheckResponse()
                {
                    DfeOrganisationCheckResult = DfeOrganisationCheckResult.Multiple
                };
            }

            return new DfeOrganisationsCheckResponse()
            {
                DfeOrganisationCheckResult = DfeOrganisationCheckResult.Single,
                Id = dfeOrganisations.Single().Id
            };
        }
    }
}