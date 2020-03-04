using System;
using System.ComponentModel;
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
        public FeedbackAnswers() { }
        
        public FeedbackAnswers(FeedbackAnswers answers)
        {
            InformationAndCommunicationBeforeVisit = answers.InformationAndCommunicationBeforeVisit;
            AskDeliveryPartnerWhoVisited = answers.AskDeliveryPartnerWhoVisited;
            ActivitiesDelivered = answers.ActivitiesDelivered;
        }

        public FeedbackRating? InformationAndCommunicationBeforeVisit { get; set; }
        public FeedbackRating? AskDeliveryPartnerWhoVisited { get; set; }
        public FeedbackRating? ActivitiesDelivered { get; set; }
        public FeedbackRating? RaisingKnowledgeAndAwareness { get; set; }
        public FeedbackRating? DemonstratingTheRangeAndOptions { get; set; }
        public FeedbackRating? ExplainingApplicationAndRecruitmentProcess { get; set; }
        public FeedbackRating? DemonstratingDifferentTypesOfEmployers { get; set; }
        public FeedbackRating? IncreasingAwarenessOfHigherAndDegree { get; set; }
        public FeedbackRating? MakingStaffMoreConfident { get; set; }
    }

    public enum FeedbackRating
    {
        Excellent, 
        Good,
        Satisfactory,
        Poor,
        [Description("Very poor")]
        VeryPoor
    }

    public enum FeedbackStatus
    {
        NotStarted,
        Complete
    }
}