using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest
{
    public class SubmitSupportRequestHandler : IRequestHandler<SubmitSupportRequest>
    {
        private readonly AskContext _context;
        private readonly ILogger<SubmitSupportRequestHandler> _logger;
        private readonly IMediator _mediator;

        public SubmitSupportRequestHandler(AskContext context, ILogger<SubmitSupportRequestHandler> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }
        
        public async Task<Unit> Handle(SubmitSupportRequest request, CancellationToken cancellationToken)
        {
            var tempSupportRequest = await _context.TempSupportRequests.SingleAsync(tsr => tsr.Id == request.TempSupportRequest.Id, cancellationToken: cancellationToken);

            // Create the real Support Request data here from the TempSupportRequest.
            
            var organisation = await _mediator.Send(new GetOrCreateOrganisationRequest(tempSupportRequest), cancellationToken);
            
            var contact = await _mediator.Send(new GetOrCreateOrganisationContactRequest(tempSupportRequest, organisation.Id));

            var supportRequest = new SupportRequest()
            {
                AdditionalComments = tempSupportRequest.AdditionalComments,
                CurrentStatus = RequestStatus.Submitted,
                Id = tempSupportRequest.Id,
                EventLogs = new List<SupportRequestEventLog>()
                {
                    new SupportRequestEventLog
                    {
                        Id = Guid.NewGuid(),
                        SupportRequestId = request.TempSupportRequest.Id, 
                        Status = RequestStatus.Draft,
                        EventDate = tempSupportRequest.StartDate,
                        Email = request.Email
                    },
                    new SupportRequestEventLog
                    {
                        Id = Guid.NewGuid(),
                        SupportRequestId = request.TempSupportRequest.Id, 
                        Status = RequestStatus.Submitted,
                        EventDate = DateTime.UtcNow,
                        Email = request.Email
                    }
                },
                Organisation = organisation,
                OrganisationContact = contact
            };

            await _context.SupportRequests.AddAsync(supportRequest, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            // Send email(s).
            
            return Unit.Value;
        }
    }
}