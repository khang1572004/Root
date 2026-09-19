using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service.Application.Common;

namespace Service.Api.ErrorHandling;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problem = exception switch
        {
            ApplicationValidationException validationException => CreateProblem(
                StatusCodes.Status400BadRequest,
                "Validation failed.",
                validationException.Message,
                httpContext,
                validationException.Errors),
            TenantNotAvailableException tenantException => CreateProblem(
                StatusCodes.Status403Forbidden,
                "Tenant access denied.",
                tenantException.Message,
                httpContext),
            NotFoundException notFoundException => CreateProblem(
                StatusCodes.Status404NotFound,
                "Resource not found.",
                notFoundException.Message,
                httpContext),
            _ => CreateUnexpectedProblem(httpContext, exception),
        };

        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }

    private static ProblemDetails CreateProblem(
        int status,
        string title,
        string detail,
        HttpContext httpContext,
        IReadOnlyDictionary<string, string[]>? errors = null)
    {
        ProblemDetails problem = errors is null
            ? new ProblemDetails()
            : new ValidationProblemDetails(new Dictionary<string, string[]>(errors));

        problem.Status = status;
        problem.Title = title;
        problem.Detail = detail;
        problem.Instance = httpContext.Request.Path;

        problem.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        return problem;
    }

    private ProblemDetails CreateUnexpectedProblem(HttpContext httpContext, Exception exception)
    {
        logger.LogError(exception, "Unhandled exception while processing {RequestPath}", httpContext.Request.Path);

        return CreateProblem(
            StatusCodes.Status500InternalServerError,
            "An unexpected error occurred.",
            "Use the trace ID when contacting support.",
            httpContext);
    }
}
