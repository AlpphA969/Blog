using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Blog.Endpiont.Api.ErrorHandling;

internal static class ApiErrorResponseWriter
{
    internal static Task WriteAsync(HttpContext context, JsonResult response) =>
        response.ExecuteResultAsync(new ActionContext(
            context, context.GetRouteData(), new ActionDescriptor()));
}