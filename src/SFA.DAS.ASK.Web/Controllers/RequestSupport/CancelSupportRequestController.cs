using System;
using System.Composition;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.CancelSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class CancelSupportRequestController : Controller
    {
        private readonly IMediator _mediator;

        public CancelSupportRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("cancel-request/{requestId}")]
        [ImportModelState]
        public  IActionResult Index(Guid requestId, string returnAction, string returnController)
        {
            var vm = new CancelSupportRequestViewModel(requestId, returnAction, returnController);
            
            return View("~/Views/RequestSupport/CancelRequest.cshtml", vm);
        }
        
        [HttpPost("cancel-request/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Cancel(Guid requestId, CancelSupportRequestViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new {requestId, vm.ReturnAction, vm.ReturnController});
            }

            if (!vm.ConfirmedCancel.Value)
            {
                return RedirectToAction(vm.ReturnAction, vm.ReturnController, new {requestId = requestId});
            }

            await _mediator.Send(new CancelSupportRequestCommand(requestId));
            return RedirectToAction("Index", "Home");

        }
    }
}