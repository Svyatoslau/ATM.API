namespace ATM.API.Models;

public sealed class Bank
{
    private static readonly ICollection<Card> Cards = new HashSet<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", CardBrand.Visa, 800),
        new ("5200000000001005", "Levi Downs", CardBrand.MasterCard, 400)
    };

    public Card GetCard(string cardNumber) => Cards.Single(x => x.Number.Equals(cardNumber));

    public void Withdraw(string cardNumber, int amount)
    {
        var card = GetCard(cardNumber);

        if (card.Balance < 0)
        {
            throw new InvalidOperationException
                ($"No cash available at the moment on the card. Sorry {card.Holder}.");
        }

        var IsLimitExceeded = amount > (int)card.Brand;
        if (IsLimitExceeded)
        {
            throw new ArgumentOutOfRangeException
                ($"You couldn't withdraw more than {(int)card.Brand} at once.");
        }

        if (card.Balance < amount)
        {
            throw new InvalidOperationException
                ($"You couldn't withdraw {amount}. Available card amount is {card.Balance}. Sorry {card.Holder}.");
        }

        card.Withdraw(amount);
    }

    
}
