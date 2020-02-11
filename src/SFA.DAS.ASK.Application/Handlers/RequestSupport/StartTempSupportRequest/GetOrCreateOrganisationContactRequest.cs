using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class GetOrCreateOrganisationContactRequest : IRequest<OrganisationContact>
    {
        public TempSupportRequest TempSupportRequest { get; }
        public Guid OrganisationId { get; set; }

        public GetOrCreateOrganisationContactRequest(TempSupportRequest tempSupportRequest, Guid organisationId)
        {
            TempSupportRequest = tempSupportRequest;
            OrganisationId = organisationId;
        }
    }
}