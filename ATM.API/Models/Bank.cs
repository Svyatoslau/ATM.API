using ATM.API.Models.Interfaces;
using ATM.API.Services;

namespace ATM.API.Models;

public sealed class Bank : IBank
{
    private static readonly ICollection<Card> Cards = new HashSet<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", CardBrand.Visa, 800, "edyDfd5A"),
        new ("5200000000001005", "Levi Downs", CardBrand.MasterCard, 400, "teEAxnqg")
    };

    private readonly ICardService _cardService;
    private int cardWithdrawLimit { get; set; }

    public Bank(ICardService cardService) => _cardService = cardService;

    public Card GetCard(string cardNumber) => Cards.Single(x => x.Number.Equals(cardNumber));

    private static int GetCardWithdrawLimit(CardBrand cardBrand) => cardBrand switch
    {
        CardBrand.MasterCard => 300,
        CardBrand.Visa => 200,
        _ => throw new ArgumentOutOfRangeException(nameof(cardBrand), $"Invalid card type."),
    };

    public void Withdraw(string cardNumber, int amount)
    {
        if (!_cardService.IsValidCardNumber(cardNumber))
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

        cardWithdrawLimit = GetCardWithdrawLimit(card.Brand);


        if (cardWithdrawLimit < amount)
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

    
}
