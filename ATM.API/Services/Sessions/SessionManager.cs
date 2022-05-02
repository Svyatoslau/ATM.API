using ATM.API.Repositories;
using ATM.API.Security;
using ATM.API.Services.Interfaces;

namespace ATM.API.Services.Sessions;

public sealed class SessionManager : ISessional
{
    private readonly SessionStorage _sessionStorage;

    private readonly ISecurityManager _securityManager;

    public SessionManager(SessionStorage sessionStorage, ISecurityManager securityManager)
        => (_sessionStorage, _securityManager) = (sessionStorage, securityManager);

    public Guid Start(string cardNumber)
    {
        var token = _securityManager.CreateToken();

        _sessionStorage.Add(new(cardNumber, token));

        return token;
    }

    public void Authorize(Guid token)
    {
        var session = _sessionStorage.Find(token);

        _sessionStorage.Remove(session);

        _sessionStorage.Add(session with { IsAuthorized = true });
    }

    public bool IsAuthorized(Guid token) => _sessionStorage.Find(token).IsAuthorized;

    public void Finish(Guid token)
    {
        var session = _sessionStorage.Find(token);

        _sessionStorage.Remove(session);
    }
    public void Receipt(Guid token, bool answer)
    {

        var session = _sessionStorage.Find(token);

        _sessionStorage.Remove(session);

        _sessionStorage.Add(session with { Receipt = answer });
    }
    public bool IsIncludeReceipt(Guid token) => _sessionStorage.Find(token).Receipt;
    public string GetCardNumber(Guid token) => _sessionStorage.Find(token).CardNumber;
}