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
        private readonly IMediator _mediator;

        public  FeedbackYourCommentsController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
            ViewName = "~/Views/Feedback/YourComments.cshtml";
            NextPageController = "FeedbackComplete";
        }

        protected override async Task PostSubmitAction(Guid feedbackId)
        {
            var feedback = await _mediator.Send(new GetVisitFeedbackRequest(feedbackId, false));

            feedback.Status = FeedbackStatus.Complete;

            await _mediator.Send(new SetVisitFeedbackCompleteCommand(feedbackId, feedback.Status));
        }
    }
}