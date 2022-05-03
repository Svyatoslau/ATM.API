using Retake.Atm.Api.Extensions;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await context
                .Response
                .WithStatusCode(StatusCodes.Status400BadRequest)
                .WithJsonContent(new { message = ex.Message });
        }
    }
}