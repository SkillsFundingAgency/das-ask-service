using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class RequestSupportController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RequestSupportController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public RequestSupportController(IMediator mediator, ILogger<RequestSupportController> logger, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }
        
        [HttpGet("request-support")]
        [ImportModelState]
        public IActionResult Index()
        {
            var value = _contextAccessor.HttpContext.Session.GetString("Dave");
            _logger.LogInformation("Value is " + value);
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
                return RedirectToAction("SignIn", "RequestSupportSignIn");
            }

            var startRequestResponse = await  _mediator.Send(new StartTempSupportRequestCommand(SupportRequestType.Manual));
            return RedirectToAction("Index", "OrganisationSearch", new {requestId = startRequestResponse.RequestId});
        }
    }
}