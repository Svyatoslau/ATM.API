using ATM.API.Services.Sessions;
using AuthenticatedWebApi.Security;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ATM.API.Security;

public class TokenService : ITokenService
{
    private readonly SessionManager _sessionManager;

    public TokenService(SessionManager sessionManager) => _sessionManager = sessionManager;

    public AuthenticationTicket ValidateToken(Guid token)
    {
        if (!_sessionManager.IsAuthorized(token))
        {
            return null;
        }

        return CreateAuthenticationTicket();
    }

    private AuthenticationTicket CreateAuthenticationTicket()
    {
        Claim[] claims = new Claim[]
        {
                new Claim(ClaimTypes.NameIdentifier, "user")
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        AuthenticationTicket authTicket = new AuthenticationTicket(claimsPrincipal, TokenAuthenticationSchemeOptions.Name);

        return authTicket;
    }
}
