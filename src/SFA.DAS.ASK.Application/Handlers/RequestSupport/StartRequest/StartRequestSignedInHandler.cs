using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class StartRequestSignedInHandler : IRequestHandler<StartRequestSignedInRequest, SupportRequest>
    {
        private readonly RequestSupportContext _context;

        public StartRequestSignedInHandler(RequestSupportContext context)
        {
            _context = context;
        }
        public async Task<SupportRequest> Handle(StartRequestSignedInRequest request, CancellationToken cancellationToken)
        {
            // Get this user's details from DfE SignIn and pre-populate new SupportRequest....
            var supportRequest = new SupportRequest
            {
                Id = Guid.NewGuid(),
                Agree = true,
                Email = "david.gouge@digital.education.gov.uk",
                BuildingAndStreet1 = "3 The Street",
                BuildingAndStreet2 = "Village-ville",
                TownOrCity = "Townly",
                County = "Countyshire",
                Postcode = "PO57 3OD",
                FirstName = "David",
                LastName = "Gouge",
                JobRole = "Dev",
                PhoneNumber = "23487234987234",
                OrganisationType = 2
            };

            _context.SupportRequests.Add(supportRequest);
            await _context.SaveChangesAsync(cancellationToken);
            
            return supportRequest;
        }
    }
}