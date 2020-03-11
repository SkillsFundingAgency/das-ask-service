using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section1ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            InformationAndCommunicationBeforeVisit = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.InformationAndCommunicationBeforeVisit, "Information and communication before the visit", "InformationAndCommunicationBeforeVisit");
            AskDeliveryPartnerWhoVisited = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.AskDeliveryPartnerWhoVisited, "The ASK delivery partner who visited", "AskDeliveryPartnerWhoVisited");
            ActivitiesDelivered = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.ActivitiesDelivered, "The activities they delivered", "ActivitiesDelivered");
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.InformationAndCommunicationBeforeVisit = InformationAndCommunicationBeforeVisit?.Rating;
            answers.AskDeliveryPartnerWhoVisited = AskDeliveryPartnerWhoVisited?.Rating;
            answers.ActivitiesDelivered = ActivitiesDelivered?.Rating;
            return answers;
        }

        public Guid FeedbackId { get; set; }
        public bool ExecuteCustomValidation(ModelStateDictionary modelState)
        {
            return true;
        }

        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel InformationAndCommunicationBeforeVisit { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel AskDeliveryPartnerWhoVisited { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel ActivitiesDelivered { get; set; }
    }
}