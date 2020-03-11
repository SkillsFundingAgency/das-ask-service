using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts
{
    public class GetOrganisationContactsRequest : IRequest<List<OrganisationContact>>
    {
        public Guid OrganisationId { get; set; }
        public GetOrganisationContactsRequest(Guid organisationId)
        {
            OrganisationId = organisationId;
        }
    }
}
