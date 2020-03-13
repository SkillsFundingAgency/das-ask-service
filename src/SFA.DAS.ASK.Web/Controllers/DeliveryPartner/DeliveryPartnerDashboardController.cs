using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner
{
    [Route("delivery-partner/dashboard")]
    public class DeliveryPartnerDashboardController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View("~/Views/DeliveryPartner/Dashboard/Dashboard.cshtml");
        }
    }
}