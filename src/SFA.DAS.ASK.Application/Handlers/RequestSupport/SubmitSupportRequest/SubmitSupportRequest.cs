using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest
{
    public class SubmitSupportRequest : IRequest
    {
        public TempSupportRequest TempSupportRequest { get; }

        public SubmitSupportRequest(TempSupportRequest tempSupportRequest)
        {
            TempSupportRequest = tempSupportRequest;
        }
    }
}