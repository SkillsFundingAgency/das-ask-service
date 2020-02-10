using MediatR;
using SFA.DAS.ASK.Application.DfeApi;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class GetOrCreateOrganisationRequest : IRequest<Organisation>
    {
        public DfeOrganisation Org { get; }

        public GetOrCreateOrganisationRequest(DfeOrganisation org)
        {
            Org = org;
        }
    }
}