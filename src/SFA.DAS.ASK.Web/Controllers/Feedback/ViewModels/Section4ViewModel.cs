using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section4ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            ApprenticeOrEmployerParticipationRating = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.ApprenticeOrEmployerParticipationRating, "How would you rate their participation", "ApprenticeOrEmployerParticipationRating");
            ApprenticeOrEmployerParticipateInVisit = feedback.FeedbackAnswers.ApprenticeOrEmployerParticipateInVisit;
            ApprenticeOrEmployerParticipateComments = feedback.FeedbackAnswers.ApprenticeOrEmployerParticipateComments;
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.ApprenticeOrEmployerParticipateInVisit = ApprenticeOrEmployerParticipateInVisit;
            answers.ApprenticeOrEmployerParticipationRating = ApprenticeOrEmployerParticipationRating.Rating;
            answers.ApprenticeOrEmployerParticipateComments = ApprenticeOrEmployerParticipateComments;

            return answers;
        }

        public Guid FeedbackId { get; set; }
        public bool ExecuteCustomValidation(ModelStateDictionary modelState)
        {
            if (ApprenticeOrEmployerParticipateInVisit == "Yes" && ApprenticeOrEmployerParticipationRating == null)
            {
                modelState.AddModelError("ApprenticeOrEmployerParticipationRating", "Select a rating for their participation");
                return false;
            }

            return true;
        }

        public FeedbackRatingRadioViewModel ApprenticeOrEmployerParticipationRating { get; set; } 
        [Required(ErrorMessage = "Select yes if an apprentice or employer participated in the visit")]
        public string ApprenticeOrEmployerParticipateInVisit { get; set; }
        public string ApprenticeOrEmployerParticipateComments { get; set; }
    }
}