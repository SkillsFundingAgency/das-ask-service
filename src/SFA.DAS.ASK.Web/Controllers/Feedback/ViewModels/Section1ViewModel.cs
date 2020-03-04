using System;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section1ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            InformationAndCommunicationBeforeVisit = new FeedbackRadioViewModel(feedback.FeedbackAnswers.InformationAndCommunicationBeforeVisit, "Information and communication before the visit", "InformationAndCommunicationBeforeVisit");
            AskDeliveryPartnerWhoVisited = new FeedbackRadioViewModel(feedback.FeedbackAnswers.AskDeliveryPartnerWhoVisited, "The ASK delivery partner who visited", "AskDeliveryPartnerWhoVisited");
            ActivitiesDelivered = new FeedbackRadioViewModel(feedback.FeedbackAnswers.ActivitiesDelivered, "The activities they delivered", "ActivitiesDelivered");
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.InformationAndCommunicationBeforeVisit = InformationAndCommunicationBeforeVisit?.Rating;
            answers.AskDeliveryPartnerWhoVisited = AskDeliveryPartnerWhoVisited?.Rating;
            answers.ActivitiesDelivered = ActivitiesDelivered?.Rating;
            return answers;
        }

        public Guid FeedbackId { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRadioViewModel InformationAndCommunicationBeforeVisit { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRadioViewModel AskDeliveryPartnerWhoVisited { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRadioViewModel ActivitiesDelivered { get; set; }
    }
}