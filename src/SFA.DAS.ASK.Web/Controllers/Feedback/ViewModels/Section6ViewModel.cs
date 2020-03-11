using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class Section6ViewModel : IFeedbackViewModel
    {
        public void Load(Guid feedbackId, VisitFeedback feedback)
        {
            FeedbackId = feedbackId;

            if (feedback.FeedbackAnswers.TypeOfSupportInTheFuture == null)
                feedback.FeedbackAnswers.TypeOfSupportInTheFuture = new TypeOfSupportInTheFuture();

            SupportForStudents = feedback.FeedbackAnswers.TypeOfSupportInTheFuture.SupportForStudents;
            ContactWithTrainingProviders = feedback.FeedbackAnswers.TypeOfSupportInTheFuture.ContactWithTrainingProviders;
            GuestSpeakers = feedback.FeedbackAnswers.TypeOfSupportInTheFuture.GuestSpeakers;
            StaffCpd = feedback.FeedbackAnswers.TypeOfSupportInTheFuture.StaffCpd;
            Resources = feedback.FeedbackAnswers.TypeOfSupportInTheFuture.Resources;
            Other = feedback.FeedbackAnswers.TypeOfSupportInTheFuture.Other;
            OtherDetails = feedback.FeedbackAnswers.TypeOfSupportInTheFuture.Other ? feedback.FeedbackAnswers.TypeOfSupportInTheFuture.OtherDetails : "";
        }

        public FeedbackAnswers ToFeedbackAnswers(FeedbackAnswers answers)
        {
            answers.TypeOfSupportInTheFuture = new TypeOfSupportInTheFuture()
            {
                SupportForStudents = SupportForStudents,
                ContactWithTrainingProviders = ContactWithTrainingProviders,
                GuestSpeakers = GuestSpeakers,
                StaffCpd = StaffCpd,
                Resources = Resources,
                Other = Other,
                OtherDetails = OtherDetails,
            };

            return answers;
        }

        public Guid FeedbackId { get; set; }
        public bool ExecuteCustomValidation(ModelStateDictionary modelState)
        {
            if (Other && string.IsNullOrWhiteSpace(OtherDetails))
            {
                modelState.AddModelError("OtherDetails", "Enter another type of support");
                return false;
            }
            
            return true;
        }

        public bool SupportForStudents { get; set; }
        public bool ContactWithTrainingProviders { get; set; }
        public bool GuestSpeakers { get; set; }
        public bool StaffCpd { get; set; }
        public bool Resources { get; set; }
        public bool Other { get; set; }
        public string OtherDetails { get; set; }
    }
}