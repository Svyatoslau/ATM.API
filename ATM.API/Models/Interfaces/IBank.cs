namespace ATM.API.Models.Interfaces;

public interface IBank
{
    public void Withdraw(string cardNumber, int amount);
    public int GetCardBalance(string cardNumber);
    public bool CardExist(string cardNumber);
}
