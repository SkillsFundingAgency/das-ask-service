using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest
{
    public class CancelSupportRequestHandler : IRequestHandler<CancelSupportRequestCommand>
    {
        private readonly AskContext _context;
        private readonly ISessionService _sessionService;

        public CancelSupportRequestHandler(AskContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }
        
        public async Task<Unit> Handle(CancelSupportRequestCommand request, CancellationToken cancellationToken)
        {
            var tempSupportRequest = await _context.TempSupportRequests.SingleAsync(tsr => tsr.Id == request.RequestId, cancellationToken: cancellationToken);
            tempSupportRequest.Status = TempSupportRequestStatus.Cancelled;
            await _context.SaveChangesAsync(cancellationToken);
            
            _sessionService.Remove("HasSignIn");
            _sessionService.Remove("TempSupportRequestId");
            _sessionService.Remove($"Searchstring-{request.RequestId}");
            _sessionService.Remove($"Searchresults-{request.RequestId}");
            
            return Unit.Value;
        }
    }
}