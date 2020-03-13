using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    [Route("feedback/section1/")]
    public class FeedbackSection1Controller : FeedbackControllerBase<Section1ViewModel>
    {
        public FeedbackSection1Controller(IMediator mediator) : base(mediator)
        {
            ViewName = "~/Views/Feedback/Section1.cshtml";
            NextPageController = "FeedbackSection2";
        }
    }
}