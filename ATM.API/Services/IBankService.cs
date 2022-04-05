using ATM.API.Models;

namespace ATM.API.Services;

public interface IBankService
{
    Card GetCard(string cardNumber);
    void Withdraw(string cardNumber, int amount);
    void VerifyCardPassword(string cardNumber, string password);
}
