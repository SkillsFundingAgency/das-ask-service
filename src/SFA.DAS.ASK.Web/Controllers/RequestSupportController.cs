using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest;
using SFA.DAS.ASK.Web.Infrastructure;
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
        [ImportModelState]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("request-support")]
        [ExportModelState]
        public async Task<IActionResult> Index(RequestSupportViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
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