using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class GetOrCreateOrganisationContactHandler : IRequestHandler<GetOrCreateOrganisationContactRequest, OrganisationContact>
    {
        private readonly RequestSupportContext _context;

        public GetOrCreateOrganisationContactHandler(RequestSupportContext context)
        {
            _context = context;
        }
        public async Task<OrganisationContact> Handle(GetOrCreateOrganisationContactRequest request, CancellationToken cancellationToken)
        {
            var contact = await _context.OrganisationContacts.SingleOrDefaultAsync(c => c.OrganisationId == request.OrganisationId && c.Email == request.Email, cancellationToken: cancellationToken);
            if (!(contact is null)) return contact;
            
            contact = new OrganisationContact
            {
                Email = request.Email,
                FirstName = request.Name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[0],
                LastName = request.Name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[1],
                Id = Guid.NewGuid(),
                OrganisationId = request.OrganisationId, 
                PhoneNumber = request.Telephone
            };

            await _context.OrganisationContacts.AddAsync(contact, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return contact;

        }
    }
}