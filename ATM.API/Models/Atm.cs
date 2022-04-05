using ATM.API.Services;

namespace ATM.API.Models;

public sealed class Atm: IAtmService
{
    private readonly IBankService _bankService;

    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }

    public Atm(IBankService bankService) => _bankService = bankService;
    
    public void Withdraw(string cardNumber, int amount)
    {
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

        _bankService.Withdraw(cardNumber, amount);
        
        TotalAmount -= amount;
    }
}

