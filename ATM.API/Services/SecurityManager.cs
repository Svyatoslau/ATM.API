using ATM.API.Services.Interfaces;

namespace ATM.API.Services;

public sealed class SecurityManager : ISecurityManager 
{
    public Guid CreateToken() => Guid.NewGuid();
}
