using System;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class InvalidLocationController : Controller
    {
        public IActionResult Index(Guid requestid)
        {
            return View("~/Views/RequestSupport/InvalidLocation.cshtml", requestid);
        }
    }
}