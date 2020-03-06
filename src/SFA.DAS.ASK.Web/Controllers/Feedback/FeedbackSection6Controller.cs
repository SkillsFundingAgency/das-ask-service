using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    [Route("feedback/section6/")]
    public class FeedbackSection6Controller : FeedbackControllerBase<Section6ViewModel>
    {
        public FeedbackSection6Controller(IMediator mediator) : base(mediator)
        {
            ViewName = "~/Views/Feedback/Section6.cshtml";
            NextPageController = "FeedbackYourComments";
        }
    }
}