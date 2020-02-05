using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class StartRequestHandler : IRequestHandler<StartRequestCommand, StartRequestResponse>
    {
        private readonly RequestSupportContext _requestSupportContext;

        public StartRequestHandler(RequestSupportContext requestSupportContext)
        {
            _requestSupportContext = requestSupportContext;
        }
        
        public async Task<StartRequestResponse> Handle(StartRequestCommand request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid();

            _requestSupportContext.SupportRequests.Add(new SupportRequest() {Id = requestId});
            await _requestSupportContext.SaveChangesAsync(cancellationToken);
            
            return new StartRequestResponse(requestId);
        }
    }
}