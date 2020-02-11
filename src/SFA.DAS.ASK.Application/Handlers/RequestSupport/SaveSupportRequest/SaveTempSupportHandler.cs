using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest
{
    public class SaveTempSupportHandler : IRequestHandler<SaveTempSupportRequest>
    {
        private readonly AskContext _askContext;

        public SaveTempSupportHandler(AskContext askContext)
        {
            _askContext = askContext;
        }
        
        public async Task<Unit> Handle(SaveTempSupportRequest request, CancellationToken cancellationToken)
        {
            await _askContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}