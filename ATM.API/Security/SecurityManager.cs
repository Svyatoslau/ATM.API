namespace ATM.API.Security;

public sealed class SecurityManager : ISecurityManager 
{
    public Guid CreateToken() => Guid.NewGuid();
}
