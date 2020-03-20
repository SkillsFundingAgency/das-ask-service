using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.Shared.CreateOrganisationContact
{
    public class CreateOrganisationContactHandler : IRequestHandler<CreateOrganisationContactCommand, OrganisationContact>
    {
        private readonly AskContext _askContext;

        public CreateOrganisationContactHandler(AskContext askContext)
        {
            _askContext = askContext;
        }

        public async Task<OrganisationContact> Handle(CreateOrganisationContactCommand request, CancellationToken cancellationToken)
        {
            var contacts = _askContext.OrganisationContacts;

            var contact = new OrganisationContact()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                OrganisationId = request.OrganisationId,
                JobRole = request.JobRole
            };

            contacts.Add(contact);

            await _askContext.SaveChangesAsync(cancellationToken);

            return contact;
        }
    }
}
