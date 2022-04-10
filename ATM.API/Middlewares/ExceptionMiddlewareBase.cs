using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Middlewares;

public abstract class ExceptionMiddlewareBase
{
    protected ExceptionObjectResult BadRequest(IActionExceptionResult? error)
        => new (StatusCodes.Status400BadRequest, error!);

    protected ExceptionObjectResult UnprocessableEntity(IActionExceptionResult? error)
        => new (StatusCodes.Status422UnprocessableEntity, error!);

    protected ExceptionObjectResult InternalServerError(IActionExceptionResult? error)
        => new (StatusCodes.Status500InternalServerError, error!);

    protected ExceptionObjectResult Unauthorized(IActionExceptionResult? error)
        => new (StatusCodes.Status401Unauthorized, error!);

    protected ExceptionObjectResult TimeOver(IActionExceptionResult? error)
        => new (StatusCodes.Status419AuthenticationTimeout, error!);
}