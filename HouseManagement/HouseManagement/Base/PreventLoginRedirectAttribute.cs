using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HouseManagement.Base;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PreventLoginRedirectAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: true })
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}