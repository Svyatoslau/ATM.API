using ATM.API.Services.Sessions;

namespace ATM.API.Middlewares;

public class VerifyAuthorizeHandlingMiddleware : VerifyAuthorizeBase
{
    private readonly RequestDelegate _next;

    private readonly SessionProvider _sessionProvider;

    public VerifyAuthorizeHandlingMiddleware(RequestDelegate next, SessionProvider sessionProvider)
        => (_next, _sessionProvider) = (next, sessionProvider);

    public Task Invoke(HttpContext httpContext)
    {
        var header = httpContext.GetRequestHeader(HeaderNameToken);

        if (!Guid.TryParse(header, out var token))
        {
            return _next(httpContext);
        }

        var session = _sessionProvider.Find(token);

        if (!session.IsAuthorized)
        {
            return _next(httpContext);
        }

        var equalCardNumbers = httpContext.HasCard(session.CardNumber);

        if (equalCardNumbers)
        {
            return _next(httpContext);
        }

        throw new BadHttpRequestException("Invalid card number in request.");
    }
}
