namespace ATM.API.Services;

public interface IAtmService
{
    public void Withdraw(int amount);
    public void SaveCard(string cardNumber);
    public void ValidCard(string cardNumber, string password);
    public void VerifyPassword(string password);
    public void CardExist(string cardNumber);
    public Guid CreateToken();
    public void ValidToken(string token);
    public void CleanToken();
}
