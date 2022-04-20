using ATM.API.Middlewares;

namespace Microsoft.AspNetCore.Http;

public static class MiddlewareExtensions
{
    /// <summary>
    /// Handling time session
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseTimeSessionHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<TimeSessionHandlingMiddleware>();
    

    /// <summary>
    /// Handling exception
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlingMiddleware>();

    /// <summary>
    /// Handling errors in request after authorization
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseVerifyAuthorizeHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<VerifyAuthorizeHandlingMiddleware>();
}
