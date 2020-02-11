using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest
{
    public class GetTempSupportRequestHandler : IRequestHandler<GetTempSupportRequest, TempSupportRequest>
    {
        private readonly AskContext _askContext;

        public GetTempSupportRequestHandler(AskContext askContext)
        {
            _askContext = askContext;
        }
        
        public async Task<TempSupportRequest> Handle(GetTempSupportRequest request, CancellationToken cancellationToken)
        {
            return await _askContext
                .TempSupportRequests
                .FirstOrDefaultAsync(sr => sr.Id == request.RequestId, cancellationToken: cancellationToken);
        }
    }
}