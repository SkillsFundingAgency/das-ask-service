using System;

namespace SFA.DAS.ASK.Data.Entities
{
    public class DeliveryPartnerContact
    {
        public Guid Id { get; set; }
        public Guid DeliveryPartnerId { get; set; }
        public DeliveryPartner DeliveryPartner { get; set; }
        public Guid SignInId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}