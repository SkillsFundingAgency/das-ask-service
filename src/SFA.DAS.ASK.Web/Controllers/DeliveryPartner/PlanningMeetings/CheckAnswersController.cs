using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class CheckAnswersController : Controller
    {
        [HttpGet("delivery-partner/planning-meeting/check-answers")]
        public IActionResult Index()
        {
            var vm = new CheckAnswersViewModel() { OrganisationName = " Test School" };

            return View("~/Views/DeliveryPartner/PlanningMeetings/CheckAnswers.cshtml", vm);
        }
    }
}