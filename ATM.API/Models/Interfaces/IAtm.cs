namespace ATM.API.Models.Interfaces;

public interface IAtm
{
    void Withdraw(string cardNumber, int amount);
}
