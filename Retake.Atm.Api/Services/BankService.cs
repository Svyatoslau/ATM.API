using Retake.Atm.Api.Models;
using Retake.Atm.Api.Interfaces.Services;

namespace Retake.Atm.Api.Services;

public sealed class BankService : IBankService
{
    private static readonly ICollection<Card> Cards = new HashSet<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };
    
    private static readonly IReadOnlyCollection<CardBrandLimit> WithdrawLimits = new List<CardBrandLimit>
    {
        new (CardBrands.Visa, 200),
        new (CardBrands.MasterCard, 300)
    };
    
    public bool HasCard(string cardNumber)
        => Cards.Any(x => x.Number == cardNumber);

    public int GetBalance(string cardNumber)
        => Cards.Single(x => x.Number == cardNumber).GetBalance();
    
    public bool VerifyPassword(string cardNumber, string cardPassword)
        => Cards.Any(x => x.Number == cardNumber && x.VerifyPassword(cardPassword));

    private static int GetWithdrawLimit(string cardBrand)
        => WithdrawLimits.Single(x => x.CardBrand == cardBrand).CardLimit;

    public void Withdraw(string cardNumber, int amount)
    {
        var card = Cards.Single(x => x.Number == cardNumber);

        var limit = GetWithdrawLimit(card.Brand);

        if (limit < amount)
        {
            throw new InvalidOperationException($"One time withdraw limit for {card.Brand} is {limit}");
        }
        
        card.Withdraw(amount);
    }
}