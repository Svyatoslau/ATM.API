using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AuthenticatedWebApi.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ATM.API.Security;

public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationSchemeOptions>
{
    public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ITokenService tokenService
        ) : base(options, logger, encoder, clock)
    {
        _tokenService = tokenService;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string authToken = this.Request.Headers[X_TOKEN];
        if (string.IsNullOrEmpty(authToken))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization header not found"));
        }

        var token = Guid.Parse(authToken);

        AuthenticationTicket authTicket = _tokenService.ValidateToken(token);
        if (authTicket == null)
        {
            return Task.FromResult(AuthenticateResult.Fail(""));
        }

        return Task.FromResult(AuthenticateResult.Success(authTicket));
    }

    private ITokenService _tokenService;
    private const string X_TOKEN = "X-Token";
}
