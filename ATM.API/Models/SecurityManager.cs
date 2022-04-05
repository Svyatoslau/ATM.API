using ATM.API.Services;

namespace ATM.API.Models;

public sealed class SecurityManager : ISecurityManager
{
    public Guid CreateToken() => Guid.NewGuid();
}

public sealed record CardSessionModel(string CardNumber, Guid Token)
{
    public bool IsAuthorized { get; init; }
}

public sealed class CardSessionManager
{
    private readonly ISecurityManager _securityManager;
    
    private readonly ICollection<CardSessionModel> _sessions = new List<CardSessionModel>();

    public CardSessionManager(ISecurityManager securityManager) => _securityManager = securityManager;

    public Guid StartSession(string cardNumber)
    {
        var token = _securityManager.CreateToken();
        
        _sessions.Add(new (cardNumber, token));
        
        return token;
    }

    public bool IsSessionValid(Guid token) => _sessions.Any(x => x.Token == token);

    public void AuthorizeSession(Guid token)
    {
        var session = _sessions.Single(x => x.Token == token);

        _sessions.Remove(session);

        _sessions.Add(session with { IsAuthorized = true });
    }

    public bool IsSessionAuthorized(Guid token) => _sessions.Any(x => x.Token == token && x.IsAuthorized);
    
    public string GetCardNumber(Guid token) => _sessions
        .Where(x => x.Token == token)
        .Select(static x => x.CardNumber)
        .Single();
    
    public void FinishSession(Guid token)
    {
        var session = _sessions.Single(x => x.Token == token);

        _sessions.Remove(session);
    }
}
