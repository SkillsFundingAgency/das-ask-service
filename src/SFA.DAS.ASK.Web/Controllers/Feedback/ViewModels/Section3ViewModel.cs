using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section3ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            DemonstratingDifferentTypesOfEmployers = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.DemonstratingDifferentTypesOfEmployers, "Demonstrating the different types of employers that offer apprenticeships", "DemonstratingDifferentTypesOfEmployers");
            IncreasingAwarenessOfHigherAndDegree = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.IncreasingAwarenessOfHigherAndDegree, "Increasing awareness of higher and degree apprenticeships", "IncreasingAwarenessOfHigherAndDegree");
            MakingStaffMoreConfident = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.MakingStaffMoreConfident, "Making staff more confident to discuss apprenticeships with students and parents", "MakingStaffMoreConfident");
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.DemonstratingDifferentTypesOfEmployers = DemonstratingDifferentTypesOfEmployers?.Rating;
            answers.IncreasingAwarenessOfHigherAndDegree = IncreasingAwarenessOfHigherAndDegree?.Rating;
            answers.MakingStaffMoreConfident = MakingStaffMoreConfident?.Rating;
            return answers;
        }

        public Guid FeedbackId { get; set; }
        public bool ExecuteCustomValidation(ModelStateDictionary modelState)
        {
            return true;
        }

        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel DemonstratingDifferentTypesOfEmployers { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel IncreasingAwarenessOfHigherAndDegree { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel MakingStaffMoreConfident { get; set; }
    }
}