using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.Shared.UpdateSupportRequest
{
    public class UpdatesSupportRequestHandler : IRequestHandler<UpdateSupportRequestCommand>
    {
        private readonly AskContext _askContext;

        public UpdatesSupportRequestHandler(AskContext askContext)
        {
            _askContext = askContext;

        }

        public async Task<Unit> Handle(UpdateSupportRequestCommand request, CancellationToken cancellationToken)
        {
            await _askContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
