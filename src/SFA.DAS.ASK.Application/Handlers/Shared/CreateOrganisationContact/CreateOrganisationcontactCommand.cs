using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.Shared.CreateOrganisationContact
{
    public class CreateOrganisationContactCommand : IRequest<OrganisationContact>
    {
        public OrganisationContact Contact { get; set; }

        public CreateOrganisationContactCommand(OrganisationContact contact)
        {
            Contact = contact;
        }
    }
}
