using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest
{
    public class GetSupportRequestHandler : IRequestHandler<GetSupportRequest, SupportRequest>
    {
        private readonly RequestSupportContext _requestSupportContext;

        public GetSupportRequestHandler(RequestSupportContext requestSupportContext)
        {
            _requestSupportContext = requestSupportContext;
        }
        
        public Task<SupportRequest> Handle(GetSupportRequest request, CancellationToken cancellationToken)
        {
            return _requestSupportContext.SupportRequests.FirstOrDefaultAsync(sr => sr.Id == request.RequestId, cancellationToken: cancellationToken);
        }
    }
}