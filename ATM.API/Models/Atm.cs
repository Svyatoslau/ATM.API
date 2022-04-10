using ATM.API.Models.Interfaces;
using ATM.API.Models.Managers;

namespace ATM.API.Models;

public sealed class Atm : IAtm
{
    private readonly IBank _bankService;
    private readonly CardSessionManager _cardSessionManager;
    
    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }

    public Atm(IBank bankService, CardSessionManager cardSessionManager)
        => (_bankService, _cardSessionManager) = (bankService, cardSessionManager);

    public void Withdraw(Guid token, int amount)
    {
        if (!_cardSessionManager.IsSessionAuthorized(token))
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
    }
}

