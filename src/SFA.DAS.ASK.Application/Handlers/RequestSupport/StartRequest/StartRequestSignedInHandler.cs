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

        public StartRequestSignedInHandler(RequestSupportContext context, IDfeSignInApiClient dfeClient)
        {
            _context = context;
            _dfeClient = dfeClient;
        }
        public async Task<SupportRequest> Handle(StartRequestSignedInRequest request, CancellationToken cancellationToken)
        {
            var organisations = _dfeClient.GetOrganisations(request.DfeSignInId);

            if (organisations == null || !organisations.Any()) throw new Exception($"User {request.DfeSignInId} doesn't have an associated organisation in DfE SignIn.");
            if (organisations.Count != 1) throw new NotImplementedException("Users with >1 Organisations not handled yet.");
                
            var org = organisations.First();
            var address = org.Address.Split(new []{Environment.NewLine}, StringSplitOptions.None);
                    
            var supportRequest = new SupportRequest
            {
                Id = Guid.NewGuid(),
                Agree = true,
                Email = request.Email,
                BuildingAndStreet1 = address[0],
                BuildingAndStreet2 = address[1],
                TownOrCity = address[2],
                County = address[3],
                Postcode = address[4],
                FirstName = request.Name.Split(new[]{" "}, StringSplitOptions.RemoveEmptyEntries)[0],
                LastName = request.Name.Split(new[]{" "}, StringSplitOptions.RemoveEmptyEntries)[1],
                PhoneNumber = org.Telephone, 
                OrganisationName = org.Name
            };

            _context.SupportRequests.Add(supportRequest);
            await _context.SaveChangesAsync(cancellationToken);
            
            return supportRequest;
        }
    }
}