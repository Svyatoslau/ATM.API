namespace ATM.API.Middlewares;

public class ExceptionResult : IActionExceptionResult
{
    public ExceptionResult(string message) => Message = message;

    public string Message { get; }
}