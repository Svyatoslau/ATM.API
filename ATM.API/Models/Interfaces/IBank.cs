using ATM.API.Models;

namespace ATM.API.Models.Interfaces;

public interface IBank
{
    Card GetCard(string cardNumber);
    void Withdraw(string cardNumber, int amount);
}
