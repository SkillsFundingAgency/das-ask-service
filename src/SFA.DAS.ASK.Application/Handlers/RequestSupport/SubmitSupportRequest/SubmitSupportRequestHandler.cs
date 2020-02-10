using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest
{
    public class SubmitSupportRequestHandler : IRequestHandler<SubmitSupportRequest>
    {
        private readonly RequestSupportContext _context;
        private readonly ILogger<SubmitSupportRequestHandler> _logger;

        public SubmitSupportRequestHandler(RequestSupportContext context, ILogger<SubmitSupportRequestHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<Unit> Handle(SubmitSupportRequest request, CancellationToken cancellationToken)
        {
            await _context.SupportRequestEventLogs.AddAsync(new SupportRequestEventLog
            {
                Id = Guid.NewGuid(),
                SupportRequestId = request.SupportRequest.Id, 
                Status = RequestStatus.Submitted,
                EventDate = DateTime.UtcNow,
                Email = request.Email
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            // Send email(s).
            _logger.LogInformation("Saved Support Request: " + JsonConvert.SerializeObject(request.SupportRequest));
            _logger.LogInformation("Events: " + JsonConvert.SerializeObject(_context.SupportRequestEventLogs.Where(srel => srel.SupportRequestId == request.SupportRequest.Id).ToList()));
            
            return Unit.Value;
        }
    }
}