using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisationContact;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest
{
    public class SubmitSupportRequestHandler : IRequestHandler<SubmitSupportRequest>
    {
        private readonly AskContext _context;
        private readonly ILogger<SubmitSupportRequestHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public SubmitSupportRequestHandler(AskContext context, ILogger<SubmitSupportRequestHandler> logger, IMediator mediator, ISessionService sessionService)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
            _sessionService = sessionService;
        }
        
        public async Task<Unit> Handle(SubmitSupportRequest request, CancellationToken cancellationToken)
        {
            var tempSupportRequest = await _context.TempSupportRequests.SingleAsync(tsr => tsr.Id == request.TempSupportRequest.Id, cancellationToken: cancellationToken);

            var organisation = await _mediator.Send(new GetOrCreateOrganisationRequest(tempSupportRequest), cancellationToken);
            var contact = await _mediator.Send(new GetOrCreateOrganisationContactRequest(tempSupportRequest, organisation.Id));

            var postcodeRegion = await _context.PostcodeRegions.SingleOrDefaultAsync(pr => pr.PostcodePrefix == Regex.Replace(tempSupportRequest.Postcode, @"(\p{L}+).*", "$1"), cancellationToken: cancellationToken);
            var deliveryArea = await _context.DeliveryAreas.SingleOrDefaultAsync(da => da.Id == postcodeRegion.DeliveryAreaId, cancellationToken: cancellationToken);
            
            var supportRequest = new SupportRequest()
            {
                AdditionalComments = tempSupportRequest.AdditionalComments,
                CurrentStatus = Status.Submitted,
                Id = tempSupportRequest.Id,
                EventLogs = new List<SupportRequestEventLog>()
                {
                    new SupportRequestEventLog
                    {
                        Id = Guid.NewGuid(),
                        SupportRequestId = request.TempSupportRequest.Id, 
                        Status = Status.Draft,
                        EventDate = tempSupportRequest.StartDate,
                        Email = request.Email
                    },
                    new SupportRequestEventLog
                    {
                        Id = Guid.NewGuid(),
                        SupportRequestId = request.TempSupportRequest.Id, 
                        Status = Status.Submitted,
                        EventDate = DateTime.UtcNow,
                        Email = request.Email
                    }
                },
                Organisation = organisation,
                OrganisationContact = contact,
                DeliveryPartnerId = deliveryArea.DeliveryPartnerId
            };

            tempSupportRequest.Status = TempSupportRequestStatus.Submitted;
            
            await _context.SupportRequests.AddAsync(supportRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _sessionService.Remove("HasSignIn");
            _sessionService.Remove("TempSupportRequestId");
            _sessionService.Remove($"Searchstring-{request.TempSupportRequest.Id}");
            _sessionService.Remove($"Searchresults-{request.TempSupportRequest.Id}");
            
            // Send email(s).
            
            return Unit.Value;
        }
    }
}