using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class PlanningContactController : Controller
    {
        [HttpGet("planning-contact")]
        public IActionResult Index()
        {
            var vm = new PlanningContactViewModel() { OrganisationName = "Test School"};
            return View("~/Views/DeliveryPartner/PlanningMeetings/PlanningContact.cshtml", vm);
        }
    }
}