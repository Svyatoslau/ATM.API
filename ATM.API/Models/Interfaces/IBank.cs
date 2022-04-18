namespace ATM.API.Models.Interfaces;

public interface IBank
{
    void Withdraw(string cardNumber, int amount);
}
