using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Models;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        public IActionResult Index()
        {
            _contextAccessor.HttpContext.Session.SetString("Dave", "HEllo");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
