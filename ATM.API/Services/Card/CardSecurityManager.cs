using ATM.API.Models.Interfaces;
using ATM.API.Repositories;
using ATM.API.Services.Interfaces;

namespace ATM.API.Services.Card;

public class CardSecurityManager : ICardSecurity
{
    private readonly CardStorage _cardStorage;

    public CardSecurityManager(CardStorage cardStorage) => _cardStorage = cardStorage;
    
    public void VerifyCardPassword(string cardNumber, string password)
    {
        var card = _cardStorage.Find(cardNumber);

        if (!card.VerifyPassword(password))
        {
            throw new UnauthorizedAccessException("Invalid password.");
        }
    }
}
