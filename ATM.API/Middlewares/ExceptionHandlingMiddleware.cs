using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ATM.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;



    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            await WriteResponceAsJsonAsync(context, Status400BadRequest, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await WriteResponceAsJsonAsync(context, Status422UnprocessableEntity, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteResponceAsJsonAsync(context, Status404NotFound, ex.Message);
        }
    }

    private async Task WriteResponceAsJsonAsync(
    HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        await context.Response.WriteAsJsonAsync(new
        {
            ActionId = Guid.NewGuid(),
            Title = "Exception",
            StatusCode = statusCode,
            Message = message
        });
    }
}

