using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class StartTempSupportRequestHandler : IRequestHandler<StartTempSupportRequestCommand, StartTempSupportRequestResponse>
    {
        private readonly AskContext _askContext;
        private readonly ISessionService _sessionService;

        public StartTempSupportRequestHandler(AskContext askContext, ISessionService sessionService)
        {
            _askContext = askContext;
            _sessionService = sessionService;
        }
        
        public async Task<StartTempSupportRequestResponse> Handle(StartTempSupportRequestCommand tempSupportRequest, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid();
            
            _sessionService.Set("Dave2", "Hola!");
            _sessionService.Set("Dave3", "YO!");
            var response = _sessionService.Get("Dave2");
            var response2 = _sessionService.Get("Dave3");

            _askContext.TempSupportRequests.Add(new TempSupportRequest()
            {
                Id = requestId, 
                StartDate = DateTime.UtcNow
            });
            await _askContext.SaveChangesAsync(cancellationToken);
            
            return new StartTempSupportRequestResponse(requestId);
        }
    }
}