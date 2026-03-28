using System.Net;
using System.Text.Json;

using LunchSync.Core.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LunchSync.Api.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorCode, message, details) = exception switch
        {
            // All DomainExceptions carry their own status + code
            ValidationException ve => (
                (int)ve.StatusCode,
                ve.ErrorCode,
                ve.Message,
                (object?)ve.Details),

            DomainException de => (
                (int)de.StatusCode,
                de.ErrorCode,
                de.Message,
                (object?)null),

            // 401 — missing/invalid JWT (thrown by ASP.NET auth pipeline or manually)
            UnauthorizedAccessException => (
                (int)HttpStatusCode.Unauthorized,
                "UNAUTHORIZED",
                "Missing/invalid JWT",
                (object?)null),

            // Catch-all — 500, don't leak internals
            _ => (
                (int)HttpStatusCode.InternalServerError,
                "INTERNAL_ERROR",
                "Đã xảy ra lỗi hệ thống",
                (object?)null)
        };

        // Log 5xx as errors, domain exceptions as warnings
        if (statusCode >= 500)
            _logger.LogError(exception, "Unhandled exception");
        else
            _logger.LogWarning(exception, "Domain exception {Code}", errorCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var body = new
        {
            error = new
            {
                code = errorCode,
                message,
                details
            }
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(body, _jsonOptions));
    }
}

// Extension for clean registration in Program.cs
public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionMiddleware>();
}
