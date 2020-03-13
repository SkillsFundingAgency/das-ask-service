using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    [Route("feedback/section3/")]
    public class FeedbackSection3Controller : FeedbackControllerBase<Section3ViewModel>
    {
        public FeedbackSection3Controller(IMediator mediator) : base(mediator)
        {
            ViewName = "~/Views/Feedback/Section3.cshtml";
            NextPageController = "FeedbackSection4";
        }
    }
}