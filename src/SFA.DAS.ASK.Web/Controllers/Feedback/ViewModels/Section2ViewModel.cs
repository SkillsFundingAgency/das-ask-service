using System;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section2ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            RaisingKnowledgeAndAwareness = new FeedbackRadioViewModel(feedback.FeedbackAnswers.RaisingKnowledgeAndAwareness, "Raising knowledge and awareness of apprenticeships", "RaisingKnowledgeAndAwareness");
            DemonstratingTheRangeAndOptions = new FeedbackRadioViewModel(feedback.FeedbackAnswers.DemonstratingTheRangeAndOptions, "Demonstrating the range of occupations and levels available", "DemonstratingTheRangeAndOptions");
            ExplainingApplicationAndRecruitmentProcess = new FeedbackRadioViewModel(feedback.FeedbackAnswers.ExplainingApplicationAndRecruitmentProcess, "Explaining the application and recruitment process for apprenticeships", "ExplainingApplicationAndRecruitmentProcess");
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.RaisingKnowledgeAndAwareness = RaisingKnowledgeAndAwareness?.Rating;
            answers.DemonstratingTheRangeAndOptions = DemonstratingTheRangeAndOptions?.Rating;
            answers.ExplainingApplicationAndRecruitmentProcess = ExplainingApplicationAndRecruitmentProcess?.Rating;
            return answers;
        }

        public Guid FeedbackId { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRadioViewModel RaisingKnowledgeAndAwareness { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRadioViewModel DemonstratingTheRangeAndOptions { get; set; }
        [Required(ErrorMessage = "Please select a rating")]
        public FeedbackRadioViewModel ExplainingApplicationAndRecruitmentProcess { get; set; }
    }
}