using System;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class StartTempSupportRequestResponse
    {
        public Guid RequestId { get; }

        public StartTempSupportRequestResponse(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}