using ATM.API.Models;

namespace ATM.API.Models;

public sealed class Card
{
    public string Number { get; init; }
    public string Holder { get; init; }
    public CardBrand Brand { get; init; }
    public int Balance { get; set; }

    public Card(string number, string holder, CardBrand brand, int balance) =>
        (Number, Holder, Brand, Balance) = (number, holder, brand, balance);
}

    