using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    [Route("feedback/section5/")]
    public class FeedbackSection5Controller : FeedbackControllerBase<Section5ViewModel>
    {
        public FeedbackSection5Controller(IMediator mediator) : base(mediator)
        {
            ViewName = "~/Views/Feedback/Section5.cshtml";
            NextPageController = "FeedbackSection6";
        }
    }
}