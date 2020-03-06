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
        public string IncorrectDetailsComments { get; set; }
    }

    public class FeedbackAnswers
    {
        public FeedbackRating? InformationAndCommunicationBeforeVisit { get; set; }
        public FeedbackRating? AskDeliveryPartnerWhoVisited { get; set; }
        public FeedbackRating? ActivitiesDelivered { get; set; }
        public FeedbackRating? RaisingKnowledgeAndAwareness { get; set; }
        public FeedbackRating? DemonstratingTheRangeAndOptions { get; set; }
        public FeedbackRating? ExplainingApplicationAndRecruitmentProcess { get; set; }
        public FeedbackRating? DemonstratingDifferentTypesOfEmployers { get; set; }
        public FeedbackRating? IncreasingAwarenessOfHigherAndDegree { get; set; }
        public FeedbackRating? MakingStaffMoreConfident { get; set; }
        public FeedbackRating? ApprenticeOrEmployerParticipationRating { get; set; }
        public string ApprenticeOrEmployerParticipateInVisit { get; set; }
        public string ApprenticeOrEmployerParticipateComments { get; set; }
        public string WouldYouRecommendAskVisits { get; set; }
        public string WouldNotRecommendAskVisitsComments { get; set; }
        public string BestThingsAboutYourVisit { get; set; }
        public string WhatCouldBeImprovedAboutYourVisit { get; set; }
        public string AddAnyOtherComments { get; set; }


        public TypeOfSupportInTheFuture TypeOfSupportInTheFuture { get; set; }
    }

    public class TypeOfSupportInTheFuture
    {
        public bool SupportForStudents { get; set; }
        public bool ContactWithTrainingProviders { get; set; }
        public bool GuestSpeakers { get; set; }
        public bool StaffCpd { get; set; }
        public bool Resources { get; set; }
        public bool Other { get; set; }
        public string OtherDetails { get; set; }

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