using ATM.API.Models;
using ATM.API.Models.API;
using System.Text.RegularExpressions;

namespace ATM.API.Services;

public sealed class CardService: ICardService
{

    public bool IsValidCardNumber(string cardNumber) => Regex.IsMatch(cardNumber, @"\d{16}");
    //public bool IsValidCardBrand(string cardBrand) => cardBrand == "Visa" || cardBrand == "MasterCard";
    //public bool IsValidCardHolder(string cardHolder) => Regex.IsMatch(cardHolder, @"[A-Z]{1}[a-z]{0,}\s{1}[A-Z]{1,}[a-z]{0,1})";
    
    //public bool IsValidCard(CardForWithdraw card)
    //{
    //    if (!IsValidCardNumber(card.Number)) return false;
    //    if (!IsValidCardBrand(card.Brand)) return false;
    //    if (IsValidCardHolder(card.Holder)) return false;
    //    return true;
    //}
}