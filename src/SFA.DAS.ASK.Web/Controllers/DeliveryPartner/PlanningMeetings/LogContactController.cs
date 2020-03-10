using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{

    public class LogContactController : Controller
    {
        [HttpGet("delivery-partner/planning-meeting/log-contact/{meetingId}")]
        public IActionResult Index(Guid meetingId)
        {
            // get planning meeting


            var viewModel = new LogContactViewModel();

            return View("~/Views/DeliveryPartner/PlanningMeetings/Logcontact.cshtml", viewModel);
        }

        [HttpPost("log-contact")]
        public IActionResult Index(LogContactViewModel viewModel)
        {
            // handler to update
            if (viewModel.SchedulePlanningMeeting == true)
            {
                return RedirectToAction();
            }

            return RedirectToAction();
        }
    }
}