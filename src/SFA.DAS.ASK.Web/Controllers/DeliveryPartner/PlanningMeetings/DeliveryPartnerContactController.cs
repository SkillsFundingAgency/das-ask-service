using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings
{
    public class DeliveryPartnerContactController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/DeliveryPartner/PlanningMeetings/DeliveryPartnerContact.cshtml");
        }
    }
}