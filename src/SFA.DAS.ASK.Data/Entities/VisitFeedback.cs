using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ASK.Data.Entities
{
    [Table("VisitFeedback")]
    public class VisitFeedback
    {
        public Guid Id { get; set; }
        public Guid VisitId { get; set; }
        public Visit Visit { get; set; }
        public FeedbackStatus Status { get; set; }
        public FeedbackAnswers FeedbackAnswers { get; set; }
    }

    public class FeedbackAnswers
    {
        public FeedbackRating? InformationAndCommunicationBeforeVisit { get; set; }
        public FeedbackRating? AskDeliveryPartnerWhoVisited { get; set; }
        public FeedbackRating? ActivitiesDelivered { get; set; }
    }

    public enum FeedbackRating
    {
        Excellent, 
        Good,
        Satisfactory,
        Poor,
        VeryPoor
    }

    public enum FeedbackStatus
    {
        NotStarted,
        Complete
    }
}