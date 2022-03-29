namespace ATM.API.Models;

public sealed class Atm
{
    private readonly Bank _bank;

    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }
    //private int WithdrawLimit { get; set; } = 200;

    public Atm(Bank bank)
    {
        //You can use the new way of throwing Exceptions:
        //
        //ArgumentNullException.ThrowIfNull(bank, nameof(bank));
        //
        //Or the new constructor syntax:
        //
        //public Atm(Bank bank) => _bank = bank ?? throw new ArgumentNullException(nameof(bank));
        
        _bank = bank ?? throw new ArgumentNullException(nameof(bank));
    }


    public void Withdraw(int amount, string cardNumber)
    {
        if (amount < 0)
        {
            //ArgumentOutOfRangeException has a different first parameter then massage
            //
            //You should use it like so:
            //throw new ArgumentOutOfRangeException(nameof(amount), "...");
            
            throw new ArgumentOutOfRangeException("You couldn't withdraw less or equal to zero.");
        }

        if (TotalAmount < 0)
        {
            throw new InvalidOperationException
                ($"No cash available at the moment in the bank. Sorry.");
        }


        if (TotalAmount< amount)
        {
            throw new InvalidOperationException
                ($"You couldn't withdraw {amount}. Available ATM amount is {TotalAmount}. Sorry.");
        }

        _bank.Withdraw(cardNumber, amount);
        
        TotalAmount -= amount;
    }
}

