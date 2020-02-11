using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class GetOrCreateOrganisationHandler : IRequestHandler<GetOrCreateOrganisationRequest, Organisation>
    {
        private readonly AskContext _context;

        public GetOrCreateOrganisationHandler(AskContext context)
        {
            _context = context;
        }
        
        public async Task<Organisation> Handle(GetOrCreateOrganisationRequest request, CancellationToken cancellationToken)
        {
            var organisation = await _context.Organisations.FirstOrDefaultAsync(org => org.UkPrn == int.Parse(request.TempSupportRequest.ReferenceId), cancellationToken: cancellationToken);
            if (organisation is  null)
            {
                organisation = new Organisation
                {
                    Id = Guid.NewGuid(),
                    BuildingAndStreet1 = request.TempSupportRequest.BuildingAndStreet1,
                    BuildingAndStreet2 = request.TempSupportRequest.BuildingAndStreet2,
                    TownOrCity = request.TempSupportRequest.TownOrCity,
                    County = request.TempSupportRequest.County,
                    Postcode = request.TempSupportRequest.Postcode,
                    OrganisationName = request.TempSupportRequest.OrganisationName,
                    UkPrn = int.Parse(request.TempSupportRequest.ReferenceId)
                };

                await _context.Organisations.AddAsync(organisation, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return organisation;
        }
    }
}