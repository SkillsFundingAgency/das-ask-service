using Microsoft.AspNetCore.Mvc.Filters;

namespace SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
}