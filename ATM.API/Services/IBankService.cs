namespace ATM.API.Services;

public interface IBankService
{
    public void Withdraw(string cardNumber, int amount);
    public void VerifyCardPassword(string cardNumber, string password);
}
