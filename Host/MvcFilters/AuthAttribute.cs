using System;
using System.Linq;
using Host.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Host.MvcFilters
{
    public sealed class AuthAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.HttpContext.RequestServices
                .GetService(typeof(ILogger<AuthAttribute>)) is ILogger<AuthAttribute> logger))
            {
                context.Result = new StatusCodeResult(500);
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("Token", out var token))
            {
                logger.LogDebug("Unauthorized received");
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!(context.HttpContext.RequestServices.GetService(typeof(Context)) is Context dbContext))
            {
                logger.LogError("dbContext is null");
                context.Result = new StatusCodeResult(500);
                return;
            }

            var session = dbContext.Sessions.FirstOrDefault(s => s.Token == token);
            if (session == null)
            {
                logger.LogDebug("Access denied");
                context.Result = new StatusCodeResult(403);
            }
            else
            {
                logger.LogDebug("Authorized successfully, user id: {0}", session.UserId);
                context.ActionArguments["userId"] = session.UserId;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}