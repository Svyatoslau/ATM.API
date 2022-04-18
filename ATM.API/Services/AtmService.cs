using ATM.API.Models.Interfaces;
using ATM.API.Services.Interfaces;
using ATM.API.Services.Sessions;

namespace ATM.API.Services;

public class AtmService : IAtmService
{
    private readonly IAtm _atm;
    private readonly ICardSecurity _cardSecurity;
    private readonly SessionManager _sessionManager;

    public AtmService(IAtm atm, ICardSecurity cardSecurity, SessionManager sessionManager)
        => (_atm, _cardSecurity, _sessionManager) = (atm, cardSecurity, sessionManager);
    public void AuthorizeSession(Guid token, string password)
    {
        var cardNumber = _sessionManager.GetCardNumber(token);
        _cardSecurity.VerifyCardPassword(cardNumber, password);

        _sessionManager.Authorize(token);
    }

    public void FinishSession(Guid token) => _sessionManager.FinishSession(token);

    public Guid StartSession(string cardNumber) => _sessionManager.Start(cardNumber);

    public void WithdrawMoney(Guid token, int amount)
    {
        if (!_sessionManager.IsAuthorized(token))
        {
            throw new UnauthorizedAccessException();
        }

        var cardNumber = _sessionManager.GetCardNumber(token);

        _atm.Withdraw(cardNumber, amount);

        _sessionManager.FinishSession(token);
    }
}
