using System;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public interface IFeedbackViewModel
    {
        void Load(Guid feedbackId, VisitFeedback feedback);
        FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers);
        Guid FeedbackId { get; set; }
    }
}