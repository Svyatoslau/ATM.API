namespace ATM.API.Middlewares;

public sealed class ExceptionHandlingMiddleware : ExceptionMiddlewareBase
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
            await context.Response
                .WriteJson(BadRequest(new ExceptionResult(ex.Message)));
        }
        catch (InvalidOperationException ex)
        {
            await context.Response
                .WriteJson(UnprocessableEntity(new ExceptionResult(ex.Message)));
        }
        catch (KeyNotFoundException ex)
        {
            await context.Response
                .WriteJson(InternalServerError(new ExceptionResult(ex.Message)));
        }
        catch (UnauthorizedAccessException ex)
        {
            await context.Response
                .WriteJson(Unauthorized(new ExceptionResult(ex.Message)));
        }
        catch (TimeoutException ex)
        {
            await context.Response
                .WriteJson(TimeOver(new ExceptionResult(ex.Message)));
        }
    }
}

