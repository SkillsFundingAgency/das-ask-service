using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    [Route("feedback/section2/")]
    public class FeedbackSection2Controller : FeedbackControllerBase<Section2ViewModel>
    {
        public FeedbackSection2Controller(IMediator mediator) : base(mediator)
        {
            ViewName = "~/Views/Feedback/Section2.cshtml";
            NextPageController = "FeedbackSection3";
        }
    }
}