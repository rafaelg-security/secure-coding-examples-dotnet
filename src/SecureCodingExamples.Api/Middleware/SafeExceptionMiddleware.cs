using System.Net;
using System.Text.Json;

namespace SecureCodingExamples.Api.Middleware;

public class SafeExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SafeExceptionMiddleware> _logger;

    public SafeExceptionMiddleware(RequestDelegate next, ILogger<SafeExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var correlationId = context.Response.Headers["X-Correlation-Id"].ToString();

            _logger.LogError(exception, "Unhandled exception. CorrelationId: {CorrelationId}", correlationId);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "An unexpected error occurred.",
                correlationId
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
