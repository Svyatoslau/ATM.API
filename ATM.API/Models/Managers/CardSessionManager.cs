using ATM.API.Models.Managers.Interfaces;

namespace ATM.API.Models.Managers;

public sealed class CardSessionManager : ISessional
{
    private readonly ISecurityManager _securityManager;

    private readonly ICardSecurity _cardSecurity;

    private readonly ICollection<CardSessionModel> _sessions = new List<CardSessionModel>();

    public CardSessionManager(ISecurityManager securityManager, ICardSecurity cardSecurity)
        => (_securityManager, _cardSecurity) = (securityManager, cardSecurity);
    
    private const double SessionExpirationTimeInMinutes = 1;

    public Guid StartSession(string cardNumber)
    {
        var token = _securityManager.CreateToken();

        _sessions.Add(new(cardNumber, token));

        return token;
    }
    
    private static bool IsSessionExpired(DateTime createdAt)
        => DateTime.UtcNow.AddMinutes(-SessionExpirationTimeInMinutes) > createdAt;
    
    public void AuthorizeSession(Guid token, string password)
    {
        var session = _sessions.Single(x => x.Token == token);

        if (IsSessionExpired(session.CreatedAt))
        {
            FinishSession(session.Token);
            
            throw new TimeoutException("Session has been expired");
        }

        //Are you sure that CardSessionManager should be responsible for
        //password checking?
        _cardSecurity.VerifyCardPassword(GetCardNumber(token), password);

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

