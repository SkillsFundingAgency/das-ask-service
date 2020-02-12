using System;

namespace SFA.DAS.ASK.Data.Entities
{
    public class DeliveryPartner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DeliveryPartnerStatus Status { get; set; }
        public int UkPrn { get; set; }
    }

    public enum DeliveryPartnerStatus
    {
        Live
    }
}