using Retake.Atm.Api.Interfaces.Services;

namespace Retake.Atm.Api.Services;

public sealed class AtmOperationService : IAtmOperationService
{
    // Each Atm has own amount of money inserted
    // To simplify work with Atm we just declare property
    private int TotalAmount { get; set; } = 1000;
    
    private readonly IBankService _bank;

    public AtmOperationService(IBankService bank) => _bank = bank;
    
    public bool HasCard(string cardNumber) => _bank.HasCard(cardNumber);

    public int GetBalance(string cardNumber) => _bank.GetBalance(cardNumber);

    public bool VerifyPassword(string cardNumber, string cardPassword) => _bank.VerifyPassword(cardNumber, cardPassword);

    public void Withdraw(string cardNumber, int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "You could not withdraw less or equal to zero");
        }

        if (TotalAmount <= 0)
        {
            throw new InvalidOperationException("No cash available");
        }

        if (TotalAmount < amount)
        {
            throw new InvalidOperationException($"Not enough cash in Atm to withdraw {amount}");
        }

        _bank.Withdraw(cardNumber, amount);
        
        TotalAmount -= amount;
    }
}