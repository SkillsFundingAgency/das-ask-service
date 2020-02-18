using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Infrastructure.Filters
{
    public class CheckRequestFilter : ActionFilterAttribute
    {
        private readonly IMediator _mediator;

        public CheckRequestFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("requestId"))
            {
                var supportRequest = _mediator.Send(new GetTempSupportRequest(Guid.Parse(context.ActionArguments["requestId"].ToString()))).Result;
                if (supportRequest == null || supportRequest.Status == TempSupportRequestStatus.Cancelled || supportRequest.Status == TempSupportRequestStatus.Submitted)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}