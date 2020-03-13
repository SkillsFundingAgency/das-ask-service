using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section5ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            WouldYouRecommendAskVisits = feedback.FeedbackAnswers.WouldYouRecommendAskVisits;
            WouldNotRecommendAskVisitsComments = feedback.FeedbackAnswers.WouldNotRecommendAskVisitsComments;
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.WouldYouRecommendAskVisits = WouldYouRecommendAskVisits;

            answers.WouldNotRecommendAskVisitsComments = answers.WouldYouRecommendAskVisits == "Yes" ? "" : WouldNotRecommendAskVisitsComments;

            return answers;
        }

        public Guid FeedbackId { get; set; }
        public bool ExecuteCustomValidation(ModelStateDictionary modelState)
        {
            if (WouldYouRecommendAskVisits != "No" || !string.IsNullOrWhiteSpace(WouldNotRecommendAskVisitsComments)) return true;
            
            modelState.AddModelError("WouldNotRecommendAskVisitsComments", "Enter why you would not recommend them");
            return false;
        }

        [Required(ErrorMessage = "Select yes if you would recommend ASK visits")]
        public string WouldYouRecommendAskVisits { get; set; }
        public string WouldNotRecommendAskVisitsComments { get; set; }
    }
}