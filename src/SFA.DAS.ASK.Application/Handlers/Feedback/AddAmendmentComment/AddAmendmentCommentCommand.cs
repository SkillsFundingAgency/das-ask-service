using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.AddAmmendmentComment
{
    public class AddAmendmentCommentCommand : IRequest
    {
        public Guid FeedbackId { get; set; }
        public string Comment { get; set; }

        public AddAmendmentCommentCommand(Guid feedbackId, string comment)
        {
            FeedbackId = feedbackId;
            Comment = comment;
        }
    }
}
