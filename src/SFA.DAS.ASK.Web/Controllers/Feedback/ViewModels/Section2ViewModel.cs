using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section2ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            RaisingKnowledgeAndAwareness = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.RaisingKnowledgeAndAwareness, "Raising knowledge and awareness of apprenticeships", "RaisingKnowledgeAndAwareness");
            DemonstratingTheRangeAndOptions = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.DemonstratingTheRangeAndOptions, "Demonstrating the range of occupations and levels available", "DemonstratingTheRangeAndOptions");
            ExplainingApplicationAndRecruitmentProcess = new FeedbackRatingRadioViewModel(feedback.FeedbackAnswers.ExplainingApplicationAndRecruitmentProcess, "Explaining the application and recruitment process for apprenticeships", "ExplainingApplicationAndRecruitmentProcess");
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.RaisingKnowledgeAndAwareness = RaisingKnowledgeAndAwareness?.Rating;
            answers.DemonstratingTheRangeAndOptions = DemonstratingTheRangeAndOptions?.Rating;
            answers.ExplainingApplicationAndRecruitmentProcess = ExplainingApplicationAndRecruitmentProcess?.Rating;
            return answers;
        }

        public Guid FeedbackId { get; set; }
        public bool ExecuteCustomValidation(ModelStateDictionary modelState)
        {
            return true;
        }

        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel RaisingKnowledgeAndAwareness { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel DemonstratingTheRangeAndOptions { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRatingRadioViewModel ExplainingApplicationAndRecruitmentProcess { get; set; }
    }
}