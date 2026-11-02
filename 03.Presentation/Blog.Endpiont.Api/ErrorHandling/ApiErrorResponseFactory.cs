using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Services.Contracts;

namespace Blog.Endpiont.Api.ErrorHandling;

internal static class ApiErrorResponseFactory
{
    // The first match is the canonical type for an HTTP status (400 uses InvalidInput).
    private static readonly (ErrorType Type, int StatusCode)[] StatusMappings =
    [
        (ErrorType.InvalidInput, StatusCodes.Status400BadRequest),
        (ErrorType.Unauthorized, StatusCodes.Status401Unauthorized),
        (ErrorType.Forbidden, StatusCodes.Status403Forbidden),
        (ErrorType.NotFound, StatusCodes.Status404NotFound),
        (ErrorType.Validation, StatusCodes.Status400BadRequest)
    ];

    internal static JsonResult Create(Error error)
    {
        var statusCode = StatusMappings.FirstOrDefault(mapping => mapping.Type == error.Type).StatusCode;
        return CreateResponse(error, statusCode == 0 ? StatusCodes.Status400BadRequest : statusCode);
    }

    internal static JsonResult Create(int statusCode, string? message = null)
    {
        var reason = ReasonPhrases.GetReasonPhrase(statusCode);
        var code = string.IsNullOrEmpty(reason) ? $"HttpError{statusCode}" : reason.Replace(" ", "");
        message ??= string.IsNullOrEmpty(reason) ? "The request failed." : reason;

        if (statusCode is >= 400 and < 500)
        {
            var type = StatusMappings.Where(mapping => mapping.StatusCode == statusCode)
                .Select(mapping => (ErrorType?)mapping.Type).FirstOrDefault() ?? ErrorType.InvalidInput;
            return CreateResponse(new Error(code, type, message), statusCode);
        }

        // ErrorType has no server-error member; preserve the existing 5xx response without type.
        return CreateResponse(new { code, message }, statusCode);
    }

    private static JsonResult CreateResponse(object error, int statusCode) =>
        new(new { error }) { StatusCode = statusCode };
}
