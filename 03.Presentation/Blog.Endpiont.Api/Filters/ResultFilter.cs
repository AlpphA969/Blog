using Blog.Endpiont.Api.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Contracts;

namespace Blog.Endpiont.Api.Filters;

public sealed class ResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is Result result)
        {
            if (result.IsSuccess)
            {
                context.Result = new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
            }
            else
            {
                context.Result = ApiErrorResponseFactory.Create(result.Error!);
            }
        }

        await next();
    }
}