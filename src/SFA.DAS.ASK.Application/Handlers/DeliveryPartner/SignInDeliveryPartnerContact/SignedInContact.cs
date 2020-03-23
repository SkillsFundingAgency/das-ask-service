using System;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact
{
    public class SignedInContact
    {
        public string DisplayName { get; set; }
        public string DeliveryPartnerName { get; set; }
        public Guid DeliveryPartnerId { get; set; }
    }
}