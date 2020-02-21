using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class StartTempSupportRequestHandler : IRequestHandler<StartTempSupportRequestCommand, StartTempSupportRequestResponse>
    {
        private readonly AskContext _askContext;

        public StartTempSupportRequestHandler(AskContext askContext)
        {
            _askContext = askContext;
        }
        
        public async Task<StartTempSupportRequestResponse> Handle(StartTempSupportRequestCommand tempSupportRequest, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid();

            _askContext.TempSupportRequests.Add(new TempSupportRequest()
            {
                Id = requestId, 
                StartDate = DateTime.UtcNow,
                SupportRequestType = tempSupportRequest.Type
            });
            await _askContext.SaveChangesAsync(cancellationToken);
            
            return new StartTempSupportRequestResponse(requestId);
        }
    }
}