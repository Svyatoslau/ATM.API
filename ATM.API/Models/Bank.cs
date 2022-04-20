using ATM.API.Models.Interfaces;
using ATM.API.Repositories;
using ATM.API.Services.Interfaces;

namespace ATM.API.Models;

public sealed class Bank : IBank
{
    private readonly CardStorage _cardStorage;
    private int cardWithdrawLimit { get; set; }

    public Bank(CardStorage cardStorage) => _cardStorage = cardStorage;

    private static int GetCardWithdrawLimit(CardBrand cardBrand) => cardBrand switch
    {
        CardBrand.MasterCard => 300,
        CardBrand.Visa => 200,
        _ => throw new ArgumentOutOfRangeException(nameof(cardBrand), $"Invalid card type."),
    };

    public void Withdraw(string cardNumber, int amount)
    {
        var card = _cardStorage.Find(cardNumber);

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

    public bool CardExist(string cardNumber) => _cardStorage.CardExist(cardNumber);

    public int GetCardBalance(string cardNumber) => _cardStorage.Find(cardNumber).Balance;
}
