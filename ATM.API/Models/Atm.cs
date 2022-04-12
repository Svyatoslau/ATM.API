using ATM.API.Models.Interfaces;
using ATM.API.Models.Managers;

namespace ATM.API.Models;

public sealed class Atm : IAtm
{
    private readonly IBank _bankService;
    private readonly SessionManager _cardSessionManager;
    
    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }

    public Atm(IBank bankService, SessionManager cardSessionManager)
        => (_bankService, _cardSessionManager) = (bankService, cardSessionManager);

    public void Withdraw(Guid token, int amount)
    {
        // Don't work with SessionManager in two logically different services
        // - AtmController
        // - Atm
        if (!_cardSessionManager.IsAuthorized(token))
        {
            throw new UnauthorizedAccessException();
        }
        
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "You couldn't withdraw less or equal to zero.");
        }

        if (TotalAmount <= 0)
        {
            throw new InvalidOperationException
                ($"No cash available at the moment in the bank. Sorry.");
        }


        if (TotalAmount < amount)
        {
            throw new InvalidOperationException
                ($"You couldn't withdraw {amount}. Available ATM amount is {TotalAmount}. Sorry.");
        }

        var cardNumber = _cardSessionManager.GetCardNumber(token);

        _bankService.Withdraw(cardNumber, amount);

        TotalAmount -= amount;
        
        _cardSessionManager.FinishSession(token);
    }
}

