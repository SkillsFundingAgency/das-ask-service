using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.DfeApi;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class StartRequestSignedInHandler : IRequestHandler<StartRequestSignedInRequest, SupportRequest>
    {
        private readonly RequestSupportContext _context;
        private readonly IDfeSignInApiClient _dfeClient;
        private readonly IMediator _mediator;

        public StartRequestSignedInHandler(RequestSupportContext context, IDfeSignInApiClient dfeClient, IMediator mediator)
        {
            _context = context;
            _dfeClient = dfeClient;
            _mediator = mediator;
        }
        public async Task<SupportRequest> Handle(StartRequestSignedInRequest request, CancellationToken cancellationToken)
        {
            var organisations = _dfeClient.GetOrganisations(request.DfeSignInId);

            if (organisations == null || !organisations.Any()) throw new Exception($"User {request.DfeSignInId} doesn't have an associated organisation in DfE SignIn.");
            if (organisations.Count != 1) throw new NotImplementedException("Users with >1 Organisations not handled yet.");
                
            var org = organisations.First();

            var organisation = await _mediator.Send(new GetOrCreateOrganisationRequest(org), cancellationToken);
            
            var supportRequest = new SupportRequest
            {
                Id = Guid.NewGuid(),
                Agree = true,
                Email = request.Email,
                FirstName = request.Name.Split(new[]{" "}, StringSplitOptions.RemoveEmptyEntries)[0],
                LastName = request.Name.Split(new[]{" "}, StringSplitOptions.RemoveEmptyEntries)[1],
                PhoneNumber = org.Telephone, 
                OrganisationId = organisation.Id
            };

            _context.SupportRequests.Add(supportRequest);
            
            await _context.SupportRequestEventLogs.AddAsync(new SupportRequestEventLog
            {
                Id = Guid.NewGuid(),
                SupportRequestId = supportRequest.Id, 
                Status = RequestStatus.Draft,
                EventDate = DateTime.UtcNow,
                Email = request.Email
            }, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return supportRequest;
        }
    }
}