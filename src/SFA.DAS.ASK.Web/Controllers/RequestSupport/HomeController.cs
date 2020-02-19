using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Models;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
