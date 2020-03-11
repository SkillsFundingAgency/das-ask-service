using MediatR;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitComplete
{ 
    public class SetVisitFeedbackCompleteCommand : IRequest
    {
        public Guid FeedbackId { get; }

        public FeedbackStatus FeedbackStatus { get; set; }

        public SetVisitFeedbackCompleteCommand(Guid feedbackId, FeedbackStatus feedbackStatus)
        {
            FeedbackId = feedbackId;
            FeedbackStatus = feedbackStatus;
        }
    }
}
