using ATM.API.Services;

namespace ATM.API.Models;

public class SecurityManager : ISecurityManager
{
    public Guid CreateToken() => Guid.NewGuid();
}
