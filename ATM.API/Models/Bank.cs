using ATM.API.Services;
using System.Text.RegularExpressions;

namespace ATM.API.Models;

public sealed class Bank : IBankService
{
    private static readonly ICollection<Card> Cards = new HashSet<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", CardBrand.Visa, 800, "edyDfd5A"),
        new ("5200000000001005", "Levi Downs", CardBrand.MasterCard, 400, "teEAxnqg")
    };
    
    //This property belongs to Bank and shouldn't be exposed to clients.
    public int CardTypeLimits { get; private set; }


    public Card GetCard(string cardNumber) => Cards.Single(x => x.Number.Equals(cardNumber));

    private static int GetCardWithdrawLimit(CardBrand cardBrand)
    {
        //Use a new switch expression syntax here.
        //
        //Don't set business rules in Enum
        switch (cardBrand)
        {
            case CardBrand.MasterCard: 
                return (int)CardBrand.MasterCard;
            case CardBrand.Visa:
                return (int)CardBrand.Visa;
            default: 
                throw new ArgumentOutOfRangeException(nameof(cardBrand), $"Invalid card type.");
        }
	
    }
    
    public void Withdraw(string cardNumber, int amount)
    {
        if (!IsValidCardNumber(cardNumber))
        {
            throw new ArgumentOutOfRangeException(nameof(cardNumber), "Invalid card number.");
        }

        var card = GetCard(cardNumber);

        if (card.Balance <= 0)
        {
            throw new InvalidOperationException(
                $"No cash available at the moment on the card." +
                $" Available amount is {card.Balance}. Sorry {card.Holder}.");
        }

        //cardWithdrawLimit
        CardTypeLimits = GetCardWithdrawLimit(card.Brand);

        
        if (CardTypeLimits < amount)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), $"You couldn't withdraw more than {(int)card.Brand} at once.");
        }

        if (card.Balance < amount)
        {
            throw new InvalidOperationException
                ($"You couldn't withdraw {amount}. Available card amount is {card.Balance}. Sorry {card.Holder}.");
        }

        card.Withdraw(amount);
    }

    private bool IsValidCardNumber(string cardNumber) => Regex.IsMatch(cardNumber, @"\d{16}");
}
