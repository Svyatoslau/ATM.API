namespace ATM.API.Services.Interfaces;


public interface ICardService
{
    bool IsValidCardNumber(string cardNumber);
}
