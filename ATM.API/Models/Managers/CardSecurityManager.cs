using ATM.API.Models.Interfaces;
using ATM.API.Models.Managers.Interfaces;

namespace ATM.API.Models.Managers;

public class CardSecurityManager : ICardSecurity
{
    private readonly IBank _bank;

    public CardSecurityManager(IBank bank) => _bank = bank;
    public void VerifyCardPassword(string cardNumber, string password)
    {
        var card = _bank.GetCard(cardNumber);

        if (!card.VerifyPassword(password))
        {
            throw new UnauthorizedAccessException("Not valid passoword.");
        }
    }
}
