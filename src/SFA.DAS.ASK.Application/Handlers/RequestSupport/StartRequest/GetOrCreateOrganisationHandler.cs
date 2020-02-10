using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class GetOrCreateOrganisationHandler : IRequestHandler<GetOrCreateOrganisationRequest, Organisation>
    {
        private readonly RequestSupportContext _context;

        public GetOrCreateOrganisationHandler(RequestSupportContext context)
        {
            _context = context;
        }
        
        public async Task<Organisation> Handle(GetOrCreateOrganisationRequest request, CancellationToken cancellationToken)
        {
            var organisation = await _context.Organisations.FirstOrDefaultAsync(org => org.UkPrn == request.Org.UkPrn, cancellationToken: cancellationToken);
            if (organisation is  null)
            {
                var address = request.Org.Address.Split(new []{Environment.NewLine}, StringSplitOptions.None);
                organisation = new Organisation
                {
                    Id = Guid.NewGuid(),
                    BuildingAndStreet1 = address[0],
                    BuildingAndStreet2 = address[1],
                    TownOrCity = address[2],
                    County = address[3],
                    Postcode = address[4],
                    OrganisationName = request.Org.Name,
                    UkPrn = request.Org.UkPrn
                };

                await _context.Organisations.AddAsync(organisation, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return organisation;
        }
    }
}