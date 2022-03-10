namespace ATM.API.Models;

public sealed class Atm
{
    int TotalAmount { get; set; } = 1000;

    static readonly int WithdrawLimit = 200;

    public void Withdraw(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("You couldn't withdraw less or equal to zero");
        }

        if (TotalAmount <= 0)
        {
            throw new ApplicationException("No cash available at this moment");
        }

        if (amount > WithdrawLimit)
        {
            throw new ApplicationException($"You couldn't withdraw more than {WithdrawLimit} at once");
        }

        var availableAmount = TotalAmount - amount;
        
        if (availableAmount < 0)
        {
            throw new ApplicationException(
                $"You couldn't withdraw {amount}. Available amount is {Math.Abs(availableAmount)}");
        }

        TotalAmount -= amount;
    }
}