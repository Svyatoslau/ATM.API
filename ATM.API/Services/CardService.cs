using System.Text.RegularExpressions;

namespace ATM.API.Services;

public class CardService : ICardService
{
    public bool IsValidCardNumber(string cardNumber) => Regex.IsMatch(cardNumber, @"\d{16}");
}
