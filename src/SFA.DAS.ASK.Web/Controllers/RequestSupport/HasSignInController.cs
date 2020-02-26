using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class HasSignInController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;

        public HasSignInController(IMediator mediator, ISessionService sessionService)
        {
            _mediator = mediator;
            _sessionService = sessionService;
        }
        
        [HttpGet("has-signin")]
        [ImportModelState]
        public IActionResult Index()
        {
            var vm = new HasSignInViewModel();
            var hasSignIn = _sessionService.Get("HasSignIn");
            if (hasSignIn != null)
            {
                vm.HasSignInAccount = bool.Parse(hasSignIn);
            }
            
            return View("~/Views/RequestSupport/HasSignIn.cshtml", vm);
        }

        [HttpPost("has-signin")]
        [ExportModelState]
        public async Task<IActionResult> Index(HasSignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            _sessionService.Set("HasSignIn", viewModel.HasSignInAccount.GetValueOrDefault().ToString());
            
            if (viewModel.HasSignInAccount.GetValueOrDefault())
            {
                return RedirectToAction("SignIn", "RequestSupportSignIn");
            }

            var cachedRequestIdString = _sessionService.Get("TempSupportRequestId");
            var requestId = cachedRequestIdString == null
                ? (await _mediator.Send(new StartTempSupportRequestCommand(SupportRequestType.Manual))).RequestId
                : Guid.Parse(cachedRequestIdString);

            _sessionService.Set("TempSupportRequestId", requestId.ToString());
            
            return RedirectToAction("Index", "YourDetails", new {requestId = requestId});
        }
    }
}