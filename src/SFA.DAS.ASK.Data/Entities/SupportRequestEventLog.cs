using System;

namespace SFA.DAS.ASK.Data.Entities
{
    public class SupportRequestEventLog
    {
        public Guid Id { get; set; }
        public Guid SupportRequestId { get; set; }
        public DateTime EventDate { get; set; }
        public Status Status { get; set; }
        public string Email { get; set; }
    }
}