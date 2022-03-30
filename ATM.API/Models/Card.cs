using ATM.API.Services;

namespace ATM.API.Models;

public sealed class Card 
{
    public string Number { get; }
    public string Holder { get; }
    public CardBrand Brand { get; }
    public int Balance { get; private set; }

    public Card(string number, string holder, CardBrand brand, int balance) =>
        (Number, Holder, Brand, Balance) = (number, holder, brand, balance);

    public void Withdraw(int amount) => Balance -= amount;
}

    