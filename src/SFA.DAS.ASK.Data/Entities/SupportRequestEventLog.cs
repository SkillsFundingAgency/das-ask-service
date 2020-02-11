using System;

namespace SFA.DAS.ASK.Data.Entities
{
    public class SupportRequestEventLog
    {
        public Guid Id { get; set; }
        public Guid SupportRequestId { get; set; }
        public DateTime EventDate { get; set; }
        public RequestStatus Status { get; set; }
        public string Email { get; set; }
    }
}