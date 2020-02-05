using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest
{
    public class SaveSupportHandler : IRequestHandler<SaveSupportRequest>
    {
        private readonly RequestSupportContext _requestSupportContext;

        public SaveSupportHandler(RequestSupportContext requestSupportContext)
        {
            _requestSupportContext = requestSupportContext;
        }
        
        public async Task<Unit> Handle(SaveSupportRequest request, CancellationToken cancellationToken)
        {
            await _requestSupportContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}