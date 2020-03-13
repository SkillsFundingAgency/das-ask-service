using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public interface IFeedbackViewModel
    {
        void Load(Guid feedbackId, VisitFeedback feedback);
        FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers);
        Guid FeedbackId { get; set; }

        bool ExecuteCustomValidation(ModelStateDictionary modelState);
    }
}