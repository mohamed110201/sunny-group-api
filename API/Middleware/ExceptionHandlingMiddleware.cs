using System.Text.Json;
using API.Common.JSend;

namespace API.Middleware;

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        var jsendBody = new JSendBody()
        {
            Status = "error",
            Message = exception.Message
        };
        response.StatusCode = StatusCodes.Status500InternalServerError;
        await response.WriteAsync(JsonSerializer.Serialize(jsendBody, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }));
    }
}