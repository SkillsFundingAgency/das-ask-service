using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback
{
    public class GetVisitFeedbackRequest : IRequest<VisitFeedback>
    {
        public GetVisitFeedbackRequest(Guid feedbackId, bool includeSubTables)
        {
            FeedbackId = feedbackId;
            IncludeSubTables = includeSubTables;
        }

        public Guid FeedbackId { get; set; }
        public bool IncludeSubTables { get; }
    }
}