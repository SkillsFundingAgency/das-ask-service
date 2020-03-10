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
        [HttpGet("delivery-partner/planning-meeting/log-contact/{requestId}")]
        public IActionResult Index(Guid requestId)
        {
            // get request


            var viewModel = new LogContactViewModel(requestId);

            return View("~/Views/DeliveryPartner/PlanningMeetings/Logcontact.cshtml", viewModel);
        }

        [HttpPost("delivery-partner/planning-meeting/log-contact")]
        public IActionResult Index(LogContactViewModel viewModel)
        {
            // handler to update


            if (viewModel.SchedulePlanningMeeting == true)
            {
                return RedirectToAction("Index", "SchedulePlanningMeeting");
            }

            return RedirectToAction();
        }
    }
}