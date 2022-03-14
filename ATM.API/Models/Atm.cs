namespace ATM.API.Models;

public sealed class Atm
{
    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }
    private int WithdrawLimit { get; set; } = 200;

    public void Withdraw(int amount, CardBrand brand)
    {
        WithdrawLimit = (int)brand;
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException("You couldn't withdraw less or equal to zero.");
        }

        if (amount > WithdrawLimit)
        {
            throw new ArgumentOutOfRangeException
                ($"You couldn't withdraw more than {WithdrawLimit} at once.");
        }

        if (TotalAmount < 0)
        {
            throw new ApplicationException
                ("No cash available at the moment.");
        }

        var atmBalance = TotalAmount - amount;

        if(atmBalance < 0)
        {
            throw new ApplicationException
                ($"You couldn't withdraw {amount}. Available amount is {TotalAmount}");
        }

        TotalAmount = atmBalance;
    }
}

