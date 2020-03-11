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

            contacts.Add(request.Contact);

            await _askContext.SaveChangesAsync(cancellationToken);

            return request.Contact;
        }
    }
}
