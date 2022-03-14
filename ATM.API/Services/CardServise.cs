using ATM.API.Models;
using ATM.API.Models.API;
using System.Text.RegularExpressions;

namespace ATM.API.Services
{
    public sealed class CardServise
    {
        public Card MakeCard(CardForWithdraw cardData)
        {
            if (!Regex.IsMatch(cardData.Number, @"\d{16}"))
            {
                throw new ArgumentException("Unavailable card number.");
            }

            if (!Regex.IsMatch(cardData.Initials, @"[A-Z]{1}[a-z]{0,}\s{1}[A-Z]{1,}[a-z]{0,1}"))
            {
                throw new ArgumentException("Unavailable user name or surname.");
            }

            if(!Regex.IsMatch(cardData.Code.ToString(), @"\d{3}"))
            {
                throw new ArgumentException("Unavailable code.");
            }

            CardBrand brand;
            if (cardData.Brand == "Visa")
            {
                brand = CardBrand.Visa;
            }
            else if (cardData.Brand == "MasterCard")
            {
                brand = CardBrand.MasterCard;
            }
            else throw new ArgumentException("Unavailable card brand.");

            return new Card(cardData.Number, cardData.Initials, brand, cardData.Code);
        }
    }
}
