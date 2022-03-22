namespace ATM.API.Models;

public sealed class Atm
{
    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }
    private int WithdrawLimit { get; set; } = 200;

    public void Withdraw(int amount, Card card)
    {
        WithdrawLimit = (int)card.Brand;
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
            throw new InvalidOperationException
                ($"No cash available at the moment in the bank. Sorry {card.Holder}.");
        }

        if (card.Balance < 0)
        {
            throw new InvalidOperationException
                ($"No cash available at the moment on the card. Sorry {card.Holder}.");
        }

        var cardBalance = card.Balance - amount;
        var atmBalance = TotalAmount - amount;

        if(atmBalance < 0 || cardBalance < 0)
        {
            var remainingBalance = cardBalance < atmBalance ? card.Balance : TotalAmount;
            throw new InvalidOperationException
                ($"You couldn't withdraw {amount}. Available amount is {remainingBalance}. Sorry {card.Holder}.");
        }

        //card.Balance = cardBalance;
        card.Withdraw(amount);
        TotalAmount = atmBalance;
    }
}

