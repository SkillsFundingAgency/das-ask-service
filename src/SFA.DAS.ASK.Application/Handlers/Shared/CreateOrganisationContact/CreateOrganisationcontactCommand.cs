using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.Shared.CreateOrganisationContact
{
    public class CreateOrganisationContactCommand : IRequest<OrganisationContact>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid OrganisationId { get; set; }
        public string JobRole { get; set; }

        public CreateOrganisationContactCommand(Guid id, string firstName, string lastName, string phoneNumber, string email, Guid organisationId, string jobRole)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            OrganisationId = organisationId;
            JobRole = jobRole;
        }
    }
}
