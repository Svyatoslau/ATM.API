namespace Retake.Atm.Api.Models;

public sealed class Card
{
    public string Number { get; }
    public string Holder { get; }
    public string Brand { get; }
    private string Password { get; init; }
    private int Balance { get; set; }

    public Card(string number, string holder, string password, string brand, int balance)
    {
        Number = number;
        Holder = holder;
        Password = password;
        Brand = brand;
        Balance = balance;
    }

    public int GetBalance() => Balance;

    public void Deposit(int amount)
        => Balance += amount;

    public void Withdraw(int amount)
    {
        if (Balance <= 0)
        {
            throw new InvalidOperationException("Your balance is less or equals zero");
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException("You don't have enough cash to withdraw {amount}");
        }

        Balance -= amount;
    }

    public bool VerifyPassword(string cardPassword)
        => Password.Equals(cardPassword, StringComparison.InvariantCulture);
}