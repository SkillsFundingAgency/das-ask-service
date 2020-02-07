using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class RequestSupportController : Controller
    {
        private readonly IMediator _mediator;

        public RequestSupportController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("request-support")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("request-support")]
        public async Task<IActionResult> Index(RequestSupportViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            if (viewModel.HasSignInAccount.GetValueOrDefault())
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            else
            {
                var startRequestResponse = await  _mediator.Send(new StartRequestCommand());
                return RedirectToAction("Index", "YourDetails", new {requestId = startRequestResponse.RequestId});
            }
        }
    }
}