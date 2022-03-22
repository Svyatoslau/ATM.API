namespace ATM.API.Middlewares;

public class ExceptionHandlingMiddleware : ExceptionMiddlewareBase
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
    }
}

