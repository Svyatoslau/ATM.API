using ATM.API.Models.Managers.Interfaces;
using ATM.API.Models.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ATM.API.Middlewares;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public sealed class TimeSessionHandlingMiddleware : TimeSessionBase
{
    private readonly RequestDelegate _next;

    private readonly ISessionNotWrite _sessions;

    public TimeSessionHandlingMiddleware(RequestDelegate next, CardSessionManager cardSessionManger) 
        => (_next, _sessions) = (next, cardSessionManger);


    public Task Invoke(HttpContext httpContext)
    {
        var headres = httpContext.Request.Headers;

        if (headres.Keys.Contains(HeaderNameToken))
        {
            var session = _sessions.GetSession(Guid.Parse(headres[HeaderNameToken]));
            if (IsSessionExpired(session.CreatedAt))
            {
                _sessions.FinishSession(session.Token);

                throw new TimeoutException("Session has been expired.");
            }

            return _next(httpContext);
        }

        return _next(httpContext);

    }
}
