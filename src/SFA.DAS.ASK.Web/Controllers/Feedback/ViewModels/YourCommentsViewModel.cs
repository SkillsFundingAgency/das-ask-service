using System;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class YourCommentsViewModel : IFeedbackViewModel
    {
        public Guid FeedbackId { get ; set; }
        public string BestThingsAboutYourVisit { get; set; }
        public string WhatCouldBeImprovedAboutYourVisit { get; set; }
        public string AddAnyOtherComments { get; set; }

        public bool ExecuteCustomValidation(ModelStateDictionary modelState)
        {
            return true;
        }

        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;
            BestThingsAboutYourVisit = feedback.FeedbackAnswers.BestThingsAboutYourVisit;
            WhatCouldBeImprovedAboutYourVisit = feedback.FeedbackAnswers.WhatCouldBeImprovedAboutYourVisit;
            AddAnyOtherComments = feedback.FeedbackAnswers.AddAnyOtherComments;
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.BestThingsAboutYourVisit = BestThingsAboutYourVisit;
            answers.WhatCouldBeImprovedAboutYourVisit = WhatCouldBeImprovedAboutYourVisit;
            answers.AddAnyOtherComments = AddAnyOtherComments;

            return answers;
        }
    }
}
