using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.GetSupportRequests
{
    public class GetSupportRequestsHandler : IRequestHandler<GetSupportRequestsRequest, GetSupportRequestsResponse>
    {
        private readonly ISessionService _sessionService;
        private readonly AskContext _dbContext;

        public GetSupportRequestsHandler(ISessionService sessionService, AskContext dbContext)
        {
            _sessionService = sessionService;
            _dbContext = dbContext;
        }
        
        public async Task<GetSupportRequestsResponse> Handle(GetSupportRequestsRequest request, CancellationToken cancellationToken)
        {
            var signedInContact = _sessionService.Get<SignedInContact>("SignedInContact");

            var supportRequestsQuery = _dbContext.SupportRequests
                .Where(sr =>
                    sr.DeliveryPartnerId == signedInContact.DeliveryPartnerId
                    && (sr.CurrentStatus == Status.NewRequest || sr.CurrentStatus == Status.Contacted)
                )
                .Include(sr => sr.Organisation)
                .Include(sr => sr.OrganisationContact)
                .Include(sr => sr.EventLogs);

            var newSupportRequests = await supportRequestsQuery
                .Where(sr => sr.CurrentStatus == Status.NewRequest)
                .ToListAsync(cancellationToken: cancellationToken);
            
            var contactedSupportRequests = await supportRequestsQuery.Where(sr => sr.CurrentStatus == Status.Contacted).ToListAsync(cancellationToken: cancellationToken);
            
            return new GetSupportRequestsResponse()
            {
                NewSupportRequests = newSupportRequests,
                ContactedSupportRequests = contactedSupportRequests,
            };
        }
    }
}