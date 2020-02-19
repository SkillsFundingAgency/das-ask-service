using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class HasSignInController : Controller
    {
        private readonly IMediator _mediator;
        public HasSignInController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("has-signin")]
        [ImportModelState]
        public IActionResult Index()
        {
            return View("~/Views/RequestSupport/HasSignIn.cshtml");
        }

        [HttpPost("has-signin")]
        [ExportModelState]
        public async Task<IActionResult> Index(HasSignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            if (viewModel.HasSignInAccount.GetValueOrDefault())
            {
                return RedirectToAction("SignIn", "RequestSupportSignIn");
            }

            var startRequestResponse = await  _mediator.Send(new StartTempSupportRequestCommand(SupportRequestType.Manual));
            return RedirectToAction("Index", "OrganisationSearch", new {requestId = startRequestResponse.RequestId});
        }
    }
}