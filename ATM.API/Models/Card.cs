using ATM.API.Services;

namespace ATM.API.Models;

public sealed class Card 
{
    public string Number { get; }
    public string Holder { get; }
    public CardBrand Brand { get; }
    public int Balance { get; private set; }
    
    private string Password { get; }

    public Card(string number, string holder, CardBrand brand, int balance, string password) =>
        (Number, Holder, Brand, Balance, Password) = (number, holder, brand, balance, password);

    public void Withdraw(int amount) => Balance -= amount;

    public bool VerifyPassword(string password) => Password.Equals(password, StringComparison.InvariantCulture);
}

    