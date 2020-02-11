using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest
{
    public class GetTempSupportRequest : IRequest<TempSupportRequest>
    {
        public Guid RequestId { get; }

        public GetTempSupportRequest(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}