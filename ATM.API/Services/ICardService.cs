namespace ATM.API.Services;

public interface ICardService
{
    bool IsValidCardNumber(string cardNumber);
}
