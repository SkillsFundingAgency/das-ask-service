using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.SaveVisitFeedback
{
    public class SaveVisitFeedbackRequest : IRequest
    {
        public Guid FeedbackId { get; }
        public FeedbackAnswers FeedbackAnswers { get; }

        public SaveVisitFeedbackRequest(Guid feedbackId, FeedbackAnswers feedbackAnswers)
        {
            FeedbackId = feedbackId;
            FeedbackAnswers = feedbackAnswers;
        }
    }
}