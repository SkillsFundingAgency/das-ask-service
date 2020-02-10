using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class GetOrCreateOrganisationContactRequest : IRequest<OrganisationContact>
    {
        public string Email { get; }
        public string Name { get; }
        public Guid OrganisationId { get; }
        public string Telephone { get; }

        public GetOrCreateOrganisationContactRequest(string email, string name, Guid organisationId, string telephone)
        {
            Email = email;
            Name = name;
            OrganisationId = organisationId;
            Telephone = telephone;
        }
    }
}