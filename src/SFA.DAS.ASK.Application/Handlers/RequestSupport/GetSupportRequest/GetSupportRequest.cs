using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest
{
    public class GetSupportRequest : IRequest<SupportRequest>
    {
        public Guid RequestId { get; }

        public GetSupportRequest(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}