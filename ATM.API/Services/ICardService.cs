using ATM.API.Models.API;

namespace ATM.API.Services;

public interface ICardService
{
    bool IsValidCardNumber(string cardNumber);
    //bool IsValidCardHolder(string cardHolder);
    //bool IsValidCardBrand(string cardBrand);
    //bool IsValidCard(CardForWithdraw card);
}
