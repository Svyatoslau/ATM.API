using ATM.API.Models;

namespace ATM.API.Services;

public interface IAtmService : ISessional
{
    bool Authorize(Guid token, string cardPassword);
    void Withdraw(Guid token, int amount);
    void SaveCard(string cardNumber);
    void ValidCard(string cardNumber, string password);
    void VerifyPassword(string password);
    void CardExist(string cardNumber);
    Guid CreateToken();
    void ValidToken(string token);
    void CleanToken();
}
