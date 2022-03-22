namespace ATM.API.Middlewares;

public sealed class ExceptionObjectResult : ExceptionResult
{
    public ExceptionObjectResult(int statusCode, IActionExceptionResult result)
        : base(result.Message) => StatusCode = statusCode;

    public int StatusCode { get; }
}