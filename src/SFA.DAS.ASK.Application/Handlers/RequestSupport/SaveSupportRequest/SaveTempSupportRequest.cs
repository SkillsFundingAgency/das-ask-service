using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest
{
    public class SaveTempSupportRequest : IRequest
    {
        public TempSupportRequest TempSupportRequest { get; }

        public SaveTempSupportRequest(TempSupportRequest tempSupportRequest)
        {
            TempSupportRequest = tempSupportRequest;
        }
    }
}