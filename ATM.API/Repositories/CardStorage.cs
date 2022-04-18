using ATM.API.Models;

namespace ATM.API.Repositories;

public class CardStorage : IRepository<Card, string>
{
    private static readonly ICollection<Card> Cards = new HashSet<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", CardBrand.Visa, 800, "edyDfd5A"),
        new ("5200000000001005", "Levi Downs", CardBrand.MasterCard, 400, "teEAxnqg")
    };
    public void Add(Card card) => Cards.Add(card);

    public Card Find(string cardNumber) => Cards.Single(x => x.Number.Equals(cardNumber));

    public void Remove(Card card) => Cards.Remove(card);
}
