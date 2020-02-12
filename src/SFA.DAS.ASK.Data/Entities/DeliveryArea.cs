using System;

namespace SFA.DAS.ASK.Data.Entities
{
    public class DeliveryArea
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public Status Status { get; set; }
        public int Ordering { get; set; }
        public Guid DeliveryPartnerId { get; set; }
    }
}