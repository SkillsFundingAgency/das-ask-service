using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Web.Infrastructure.Filters;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class CheckAnswersController : Controller
    {
        private readonly IMediator _mediator;

        public CheckAnswersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("check-answers/{requestId}")]
        [ServiceFilter(typeof(CheckRequestFilter))]
        public async Task<IActionResult> Index(Guid requestId)
        {
            var supportRequest = await _mediator.Send(new GetTempSupportRequest(requestId));

            var vm = new CheckAnswersViewModel(supportRequest);

            return View("~/Views/RequestSupport/CheckAnswers.cshtml", vm);
        }
    }
}
