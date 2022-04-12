﻿namespace ATM.API.Middlewares.Extensions;

public static class MiddlewareExtensions
{
    /// <summary>
    /// Middleware for handling time session
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseTimeSessionHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<TimeSessionHandlingMiddleware>();
    

    /// <summary>
    /// Middleware for handling exception
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlingMiddleware>();
    
    public static string GetRequestHeader(this HttpContext ctx, string header)
        => ctx.Request.Headers.TryGetValue(header, out var headers)
            ? headers[0]
            : string.Empty;
    
}
