using System.Text.Json;
using csharp_chat_api.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace csharp_url_shortener_api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var statusCode = GetStatusCode(error);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Type = $"https://httpstatuses.com/{statusCode}",
                Title = GetTitle(error),
                Status = statusCode,
                Detail = GetDetail(error),
                Instance = context.Request.Path
            };

            var result = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(result);
        }
    }

    private static int GetStatusCode(Exception error)
    {
        return error switch
        {
            BadRequestException => 400,
            UnauthorizedAccessException => 401,
            ForbiddenException => 403,
            NotFoundException => 404,
            ConflictException => 409,
            UnprocessableEntityException => 422,
            ServiceUnavailableException => 503,
            _ => 500
        };
    }

    private static string GetTitle(Exception error)
    {
        return error switch
        {
            BadRequestException => "Bad Request",
            UnauthorizedAccessException => "Unauthorized",
            ForbiddenException => "Forbidden",
            NotFoundException => "Not Found",
            ConflictException => "Conflict",
            UnprocessableEntityException => "Unprocessable Entity",
            ServiceUnavailableException => "Service Unavailable",
            _ => "Internal Server Error"
        };
    }

    private static string GetDetail(Exception error)
    {
        return error.InnerException != null
            ? $"{error.Message} | Inner Exception: {error.InnerException.Message}"
            : error.Message ?? "An unexpected error occurred";
    }
}