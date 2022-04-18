using ATM.API.Models;

namespace ATM.API.Repositories;

public sealed class SessionStorage : IRepository<CardSessionModel, Guid>
{
    private readonly ICollection<CardSessionModel> _sessions = new List<CardSessionModel>();

    public CardSessionModel Find(Guid token) => _sessions.Single(x => x.Token == token);

    public void Add(CardSessionModel session) => _sessions.Add(session);

    public void Remove(CardSessionModel session) => _sessions.Remove(session);
}