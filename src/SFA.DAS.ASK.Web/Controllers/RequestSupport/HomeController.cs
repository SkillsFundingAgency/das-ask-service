using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Web.Models;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public HomeController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> NewRequest()
        {
            var requestId = (await _mediator.Send(new StartTempSupportRequestCommand())).RequestId;

            _sessionService.Set("TempSupportRequestId", requestId.ToString());

            return RedirectToAction("Index", "YourDetails", new { requestId = requestId});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
