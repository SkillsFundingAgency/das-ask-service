using System;
using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest
{
    public class CancelSupportRequestCommand : IRequest
    {
        public Guid RequestId { get; }

        public CancelSupportRequestCommand(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}