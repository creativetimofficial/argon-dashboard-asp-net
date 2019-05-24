using Microsoft.AspNetCore.Mvc.Filters;

namespace CreativeTim.Argon.DotNetCore.Free.Infrastructure.ErrorHandling
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
}