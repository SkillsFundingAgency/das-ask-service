using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Application.Handlers.Feedback.SetVisitComplete;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    [Route("feedback/your-comments/")]
    public class FeedbackYourCommentsController : FeedbackControllerBase<YourCommentsViewModel>
    {
        public FeedbackYourCommentsController(IMediator mediator) : base(mediator)
        {
            ViewName = "~/Views/Feedback/YourComments.cshtml";
            NextPageController = "FeedbackComplete";
            PostSubmitAction = feedbackId => { 
                UpdateFeedbackStatus(feedbackId, mediator);
            };
        }

        private async Task UpdateFeedbackStatus(Guid feedbackId, IMediator mediator)
        {
            var feedback = await mediator.Send(new GetVisitFeedbackRequest(feedbackId, false));

            feedback.Status = FeedbackStatus.Complete;

            await mediator.Send(new SetVisitFeedbackCompleteCommand(feedbackId, feedback.Status));

        }
    }
}