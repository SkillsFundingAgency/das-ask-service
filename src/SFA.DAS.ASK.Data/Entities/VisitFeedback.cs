using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ASK.Data.Entities
{
    [Table("VisitFeedback")]
    public class VisitFeedback
    {
        public FeedbackStatus Status { get; set; }
        public Guid Id { get; set; }
    }
    
    

    public enum FeedbackStatus
    {
        NotStarted,
        Complete
    }
}