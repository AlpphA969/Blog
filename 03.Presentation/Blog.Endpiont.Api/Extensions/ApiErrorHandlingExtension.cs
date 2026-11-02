using Blog.Endpiont.Api.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Endpiont.Api.Extensions;

public static class ApiErrorHandlingExtension
{
    public static IServiceCollection AddApiErrorHandling(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            // Leave empty MVC errors (such as 415) for the status-code middleware.
            options.SuppressMapClientErrors = true;
            options.InvalidModelStateResponseFactory = ValidationErrorResponseFactory.Create;
        });
        return services;
    }

    public static IApplicationBuilder UseApiErrorHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApiExceptionMiddleware>();
        app.UseStatusCodePages(async statusContext =>
        {
            var context = statusContext.HttpContext;
            if (!context.Request.Path.StartsWithSegments("/api"))
                return;

            await ApiErrorResponseWriter.WriteAsync(context,
                ApiErrorResponseFactory.Create(context.Response.StatusCode));
        });
        return app;
    }
}