using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest
{
    public class CancelSupportRequestHandler : IRequestHandler<CancelSupportRequestCommand>
    {
        private readonly AskContext _context;

        public CancelSupportRequestHandler(AskContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(CancelSupportRequestCommand request, CancellationToken cancellationToken)
        {
            var tempSupportRequest = await _context.TempSupportRequests.SingleAsync(tsr => tsr.Id == request.RequestId, cancellationToken: cancellationToken);
            tempSupportRequest.Status = TempSupportRequestStatus.Cancelled;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}