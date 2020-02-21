using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisationContact
{
    public class GetOrCreateOrganisationContactHandler : IRequestHandler<GetOrCreateOrganisationContactRequest, OrganisationContact>
    {
        private readonly AskContext _context;

        public GetOrCreateOrganisationContactHandler(AskContext context)
        {
            _context = context;
        }
        public async Task<OrganisationContact> Handle(GetOrCreateOrganisationContactRequest request, CancellationToken cancellationToken)
        {
            var contact = await _context.OrganisationContacts.SingleOrDefaultAsync(c => c.OrganisationId == request.OrganisationId && c.Email == request.TempSupportRequest.Email, cancellationToken: cancellationToken);
            if (!(contact is null)) return contact;
            
            contact = new OrganisationContact
            {
                Email = request.TempSupportRequest.Email,
                FirstName = request.TempSupportRequest.FirstName,
                LastName = request.TempSupportRequest.LastName,
                Id = Guid.NewGuid(),
                OrganisationId = request.OrganisationId, 
                PhoneNumber = request.TempSupportRequest.PhoneNumber
            };

            await _context.OrganisationContacts.AddAsync(contact, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return contact;

        }
    }
}