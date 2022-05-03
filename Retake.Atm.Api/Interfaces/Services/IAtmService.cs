namespace Retake.Atm.Api.Interfaces.Services;

public interface IAtmService
{
    void Init(string cardNumber);
    void Authorize(string cardNumber, string cardPassword);
    int GetBalance(string cardNumber);
    void Withdraw(string cardNumber, int amount);
}