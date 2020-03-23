using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts
{
    public class GetDeliveryPartnerContactsRequest : IRequest<List<DeliveryPartnerContact>>
    {
        public Guid DeliveryPartnerOrganisationId { get; set; }

        public GetDeliveryPartnerContactsRequest(Guid deliveryPartnerOrganisationId)
        {
            DeliveryPartnerOrganisationId = deliveryPartnerOrganisationId;
        }
    }
}
