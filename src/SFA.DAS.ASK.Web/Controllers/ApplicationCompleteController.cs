using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class ApplicationCompleteController : Controller
    {
        public IActionResult Index()
        {
            var vm = new ApplicationCompleteViewModel(){Email = "davegouge@gmail.com"};
            return View("~/Views/RequestSupport/ApplicationComplete.cshtml", vm);
        }
    }

    public class ApplicationCompleteViewModel
    {
        public string Email { get; set; }
    }
}