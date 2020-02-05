using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class OtherDetailsController : Controller
    {
        [HttpGet("other-details")]
        public IActionResult Index()
        {
            var vm = new OtherDetailsViewModel();
            
            return View("~/Views/RequestSupport/OtherDetails.cshtml", vm);
        }

        [HttpPost("other-details")]
        public IActionResult Index(OtherDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/RequestSupport/OtherDetails.cshtml", viewModel);
            }

            return RedirectToAction("Index", "ApplicationComplete");
        }
        
        
    }
}