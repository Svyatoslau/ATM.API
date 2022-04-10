using ATM.API.Models.Managers.Interfaces;

namespace ATM.API.Models.Managers;

public sealed class CardSessionManager : ISessional
{
    private readonly ISecurityManager _securityManager;

    private readonly ICardSecurity _cardSecurity;

    private readonly ICollection<CardSessionModel> _sessions = new List<CardSessionModel>();

    public CardSessionManager(ISecurityManager securityManager, ICardSecurity cardSecurity)
        => (_securityManager, _cardSecurity) = (securityManager, cardSecurity);

    private int SessionTime { get;} = 60;

    public Guid StartSession(string cardNumber)
    {
        var token = _securityManager.CreateToken();

        _sessions.Add(new(cardNumber, token, DateTime.Now));

        return token;
    }

    public void CheckSessionTime(Guid token)
    {
        var oldTime = GetCreationTime(token);
        var newTime = DateTime.Now;
        var diffrence = newTime - oldTime;

        if(diffrence.TotalSeconds > SessionTime)
        {
            FinishSession(token);
            throw new TimeoutException("Session time over.");
        }
    }
    public bool IsSessionValid(Guid token) => _sessions.Any(x => x.Token == token);

    public void AuthorizeSession(Guid token, string password)
    {
        CheckSessionTime(token);

        var session = _sessions.Single(x => x.Token == token);

        _cardSecurity.VerifyCardPassword(GetCardNumber(token), password);

        _sessions.Remove(session);

        _sessions.Add(session with { IsAuthorized = true });
    }

    public bool IsSessionAuthorized(Guid token) => _sessions.Any(x => x.Token == token && x.IsAuthorized);

    public DateTime GetCreationTime(Guid token) => _sessions
        .Where(x => x.Token == token)
        .Select(x => x.CreationTime)
        .Single();
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

