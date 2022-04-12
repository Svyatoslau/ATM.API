using ATM.API.Models.Managers;
using ATM.API.Middlewares.Extensions;

namespace ATM.API.Middlewares;

public sealed class TimeSessionHandlingMiddleware : TimeSessionBase
{
    private readonly RequestDelegate _next;

    private readonly SessionProvider _sessionProvider;

    public TimeSessionHandlingMiddleware(RequestDelegate next, SessionProvider sessionProvider) 
        => (_next, _sessionProvider) = (next, sessionProvider);
    
    // Here you just read a session and don't modify it
    public Task Invoke(HttpContext httpContext)
    {
        var header = httpContext.GetRequestHeader(HeaderNameToken);

        if (!Guid.TryParse(header, out var token))
        {
            return _next(httpContext);
        }
        
        var session = _sessionProvider.Find(token);

        if (!IsSessionExpired(session.CreatedAt))
        {
            return _next(httpContext);
        }

        throw new TimeoutException("Session has been expired");
    }
}
