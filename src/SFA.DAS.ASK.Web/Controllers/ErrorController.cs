using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Web.Models;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }
}