namespace ATM.API.Services;

public interface IBankService
{
    public void Withdraw(string cardNumber, int amount);
}
