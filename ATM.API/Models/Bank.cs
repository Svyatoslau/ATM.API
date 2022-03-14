namespace ATM.API.Models;

public sealed class Bank
{
    private static readonly ICollection<Card> Cards = new HashSet<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", CardBrand.Visa, 800),
        new ("5200000000001005", "Levi Downs", CardBrand.MasterCard, 400)
    };

    public Card GetCard(string cardNumber) => Cards.Single(x => x.Number.Equals(cardNumber));
    
    public void CardCheck(Card card)
    {
        var _card = Cards.FirstOrDefault(c => c == card);

        if(_card == null)
        {
            throw new KeyNotFoundException("Card didn't found.");
        }
    }
}
