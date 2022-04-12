using ATM.API.Models.Managers.Interfaces;

namespace ATM.API.Models.Managers;

// This may also be implementation of Repository pattern
public sealed class SessionStorage
{
    private readonly ICollection<CardSessionModel> _sessions = new List<CardSessionModel>();
    
    public CardSessionModel Find(Guid token) => _sessions.Single(x => x.Token == token);

    public void Add(CardSessionModel session) => _sessions.Add(session);

    public void Remove(CardSessionModel session) => _sessions.Remove(session);
}

// With use of such provider you can make you session readonly
// Also you decouple your middleware logic from application logic
public sealed class SessionProvider
{
    private readonly SessionStorage _storage;

    public SessionProvider(SessionStorage storage) => _storage = storage;

    public CardSessionModel Find(Guid token) => _storage.Find(token);
}

// This class is for your application logic
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

