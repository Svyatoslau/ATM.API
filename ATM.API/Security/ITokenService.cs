using Microsoft.AspNetCore.Authentication;

namespace ATM.API.Security;

public interface ITokenService
{
    AuthenticationTicket ValidateToken(Guid token);
}

