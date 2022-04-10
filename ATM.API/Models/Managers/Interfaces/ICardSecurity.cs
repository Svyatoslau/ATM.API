namespace ATM.API.Models.Managers.Interfaces;

public interface ICardSecurity
{
    public void VerifyCardPassword(string cardNumber, string password);
}
