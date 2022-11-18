using Microsoft.AspNetCore.Mvc.Filters;

namespace LoadBalancer.IdenityServer.Middlewares
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();
                return;
            }

            await next();
        }
    }
}
