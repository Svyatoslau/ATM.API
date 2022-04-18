namespace ATM.API.Services.Interfaces;


public interface ICardSecurity
{
    public void VerifyCardPassword(string cardNumber, string password);
}
