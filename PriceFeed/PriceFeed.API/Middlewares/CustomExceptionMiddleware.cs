using PriceFeed.Domain.Exceptions.Base;
using System.Net;
using System.Text.Json;

namespace PriceFeed.API.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (e is BaseException exception)
            {
                _logger.LogError(e, "{Message}", exception.Message);

                response.StatusCode = (int)exception.StatusCode;
                var result = JsonSerializer.Serialize(new { message = exception.Message });

                await response.WriteAsync(result);
            }
            else
            {
                _logger.LogError(e, "Unhandled exception occurred.");

                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(new { message = e.Message });

                await response.WriteAsync(result);
            }
        }
    }
}
