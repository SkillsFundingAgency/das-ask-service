using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.ASK.Data;

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
            request.SupportRequest.Submitted = true;
            request.SupportRequest.SubmittedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            
            // Send email(s).
            _logger.LogInformation("Saved Support Request: " + JsonConvert.SerializeObject(request.SupportRequest));
            
            return Unit.Value;
        }
    }
}