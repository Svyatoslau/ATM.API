namespace ATM.API.Services.Interfaces.Atm;

public interface IMoneyOperations
{
    public void WithdrawMoney(Guid token, int amount);
    public int GetCardBalance(Guid token);
}
