using ATM.API.Models.API;

namespace ATM.API.Models;

public sealed class Bank
{
    public ICollection<Card> Cards { get; set; } = new HashSet<Card>
    {
        new Card("4444333322221111", "Troy Mcfarland", CardBrand.Visa, 800),
        new Card("5200000000001005", "Levi Downs", CardBrand.MasterCard, 400)
    };

    public void CardCheck(Card card)
    {
        var _card = Cards.FirstOrDefault(c => c == card);

        if(_card == null)
        {
            throw new KeyNotFoundException("Card didn't found.");
        }
    }
}
