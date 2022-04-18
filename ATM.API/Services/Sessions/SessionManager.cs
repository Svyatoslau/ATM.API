using ATM.API.Repositories;
using ATM.API.Services.Interfaces;

namespace ATM.API.Services.Sessions;

public sealed class SessionManager
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

    public void FinishSession(Guid token)
    {
        var session = _sessionStorage.Find(token);

        _sessionStorage.Remove(session);
    }

    public string GetCardNumber(Guid token) => _sessionStorage.Find(token).CardNumber;
}