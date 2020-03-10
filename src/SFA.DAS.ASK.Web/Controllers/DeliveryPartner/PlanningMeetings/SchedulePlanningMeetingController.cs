using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class SchedulePlanningMeetingController : Controller
    {
        [HttpGet("schedule-planning-meeting")]
        public IActionResult Index()
        {
            return View("~/Views/DeliveryPartner/PlanningMeetings/SchedulePlanningMeeting.cshtml");
        }
    }
}