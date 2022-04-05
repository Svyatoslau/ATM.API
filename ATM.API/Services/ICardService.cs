namespace ATM.API.Services;

public interface ICardService
{
    public bool IsValidCardNumber(string cardNumber);
}
