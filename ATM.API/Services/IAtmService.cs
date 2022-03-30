namespace ATM.API.Services;

public interface IAtmService
{
    public void Withdraw(string cardNumber, int amount);
}
