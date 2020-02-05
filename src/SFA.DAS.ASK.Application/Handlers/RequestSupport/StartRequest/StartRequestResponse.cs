using System;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class StartRequestResponse
    {
        public Guid RequestId { get; }

        public StartRequestResponse(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}