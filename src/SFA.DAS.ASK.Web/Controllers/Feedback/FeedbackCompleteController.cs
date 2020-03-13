using System;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    public class FeedbackCompleteController : Controller
    {
        [HttpGet("/feedback/complete/{feedbackId}")]
        public IActionResult Index(Guid feedbackId)
        {
            return View("~/Views/Feedback/Complete.cshtml");
        }
    }
}