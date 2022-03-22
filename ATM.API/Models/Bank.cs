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
        //
        // 1. Check card balance
        // 2. Check card withdraw limit
        // 3. Withdraw money from card
        // 4. Track succeeded transaction
        // 5. If withdraw fails, track failed transaction
        //
    }
}
