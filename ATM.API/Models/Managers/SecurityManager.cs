using ATM.API.Models.Managers.Interfaces;

namespace ATM.API.Models.Managers;

public sealed class SecurityManager : ISecurityManager
{
    public Guid CreateToken() => Guid.NewGuid();
}
