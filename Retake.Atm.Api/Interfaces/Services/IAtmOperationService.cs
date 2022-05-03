namespace Retake.Atm.Api.Interfaces.Services;

public interface IAtmOperationService
{
    bool HasCard(string cardNumber);
    int GetBalance(string cardNumber);
    bool VerifyPassword(string cardNumber, string cardPassword);
    void Withdraw(string cardNumber, int amount);
}