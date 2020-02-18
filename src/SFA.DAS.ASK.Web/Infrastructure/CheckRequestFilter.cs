using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SFA.DAS.ASK.Web.Infrastructure
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
            base.OnActionExecuting(context);
        }
    }
}