using System.Diagnostics;

namespace basic_delivery_api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        var endpoint = context.GetEndpoint();
        var endpointName = endpoint?.DisplayName ?? "Unknown";
        var statusCode = context.Response.StatusCode;

        if (statusCode >= 400)
        {
            _logger.LogError("Endpoint: {EndpointName}, Timestamp: {Timestamp}, StatusCode: {StatusCode}, ElapsedMilliseconds: {ElapsedMilliseconds}",
                endpointName,
                DateTime.UtcNow,
                statusCode,
                stopwatch.ElapsedMilliseconds);
        }
        else
        {
            _logger.LogInformation("Endpoint: {EndpointName}, Timestamp: {Timestamp}, StatusCode: {StatusCode}, ElapsedMilliseconds: {ElapsedMilliseconds}",
                endpointName,
                DateTime.UtcNow,
                statusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}