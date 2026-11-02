using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Blog.Endpiont.Api.ErrorHandling;

internal static class ValidationErrorResponseFactory
{
    internal static JsonResult Create(ActionContext context)
    {
        var messages = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .SelectMany(entry => entry.Value!.Errors.Select(error =>
                string.IsNullOrWhiteSpace(error.ErrorMessage)
                    ? "The submitted value is invalid."
                    : error.ErrorMessage));
        var validationMessage = string.Join("\n", messages);

        return ApiErrorResponseFactory.Create(new Error(
            "ValidationFailed", ErrorType.Validation,
            string.IsNullOrWhiteSpace(validationMessage)
                ? "The submitted data is invalid."
                : validationMessage));
    }
}