using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class GetOrCreateOrganisationRequest : IRequest<Organisation>
    {
        public TempSupportRequest TempSupportRequest { get; }

        public GetOrCreateOrganisationRequest(TempSupportRequest tempSupportRequest)
        {
            TempSupportRequest = tempSupportRequest;
        }
    }
}