using ATM.API.Models;
using ATM.API.Repositories;

namespace ATM.API.Services.Sessions;

public sealed class SessionProvider
{
    private readonly SessionStorage _storage;

    public SessionProvider(SessionStorage storage) => _storage = storage;

    public CardSessionModel Find(Guid token) => _storage.Find(token);
}
