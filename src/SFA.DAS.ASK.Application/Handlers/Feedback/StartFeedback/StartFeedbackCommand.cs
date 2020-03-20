using System;
using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.StartFeedback
{
    public class StartFeedbackCommand : IRequest
    {
        public Guid FeedbackId { get; }

        public StartFeedbackCommand(Guid feedbackId)
        {
            FeedbackId = feedbackId;
        }
    }
}