using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest
{
    public class GetSupportRequest : IRequest<SupportRequest>
    {
        public Guid RequestId { get; set; }
        
        public GetSupportRequest(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}
