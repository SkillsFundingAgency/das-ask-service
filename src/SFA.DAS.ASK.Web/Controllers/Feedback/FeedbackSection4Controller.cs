using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    [Route("feedback/section4/")]
    public class FeedbackSection4Controller : FeedbackControllerBase<Section4ViewModel>
    {
        public FeedbackSection4Controller(IMediator mediator) : base(mediator)
        {
            ViewName = "~/Views/Feedback/Section4.cshtml";
            NextPageController = "FeedbackSection5";
        }
    }
}