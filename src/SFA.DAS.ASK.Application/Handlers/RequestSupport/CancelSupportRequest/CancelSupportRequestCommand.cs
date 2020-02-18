using System;
using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest
{
    public class CancelSupportRequestCommand : IRequest
    {
        public Guid RequestId { get; }
        public string Email { get; }

        public CancelSupportRequestCommand(Guid requestId, string email)
        {
            RequestId = requestId;
            Email = email;
        }
    }
}