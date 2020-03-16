using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContact
{
    public class GetDeliveryPartnerContactRequest : IRequest<DeliveryPartnerContact>
    {
        public Guid DeliveryPartnerContactId { get; set; }

        public GetDeliveryPartnerContactRequest(Guid deliveryPartnerContactId)
        {
            DeliveryPartnerContactId = deliveryPartnerContactId;
        }
    }
}
