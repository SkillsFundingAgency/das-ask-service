using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.Feedback
{
    public class YourCommentsViewModel
    {
        public Guid FeedbackId { get; set; }

        public string CommentsBestThings { get; set; }
        public string CommentsCouldBeImproved { get; set; }
        public string CommentsAdditional { get; set; }
        public YourCommentsViewModel()
        {

        }
        public YourCommentsViewModel(VisitFeedback feedback)
        {
            FeedbackId = feedback.Id;

            CommentsBestThings = "";
            CommentsCouldBeImproved = "";
            CommentsAdditional = "Additional comments";
        }
    }
}
