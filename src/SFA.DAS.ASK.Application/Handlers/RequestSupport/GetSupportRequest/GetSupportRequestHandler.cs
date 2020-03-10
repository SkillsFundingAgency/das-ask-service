using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest
{
    public class GetSupportRequestHandler : IRequestHandler<GetSupportRequest, SupportRequest>
    {
        private readonly AskContext _askContext;

        public GetSupportRequestHandler(AskContext askContext)
        {
            _askContext = askContext;
        }

        public async Task<SupportRequest> Handle(GetSupportRequest request, CancellationToken cancellationToken)
        {
            return await _askContext
                .SupportRequests
                .FirstOrDefaultAsync(sr => sr.Id == request.RequestId, cancellationToken: cancellationToken);
        }
    }
}
