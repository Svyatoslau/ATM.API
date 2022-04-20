namespace ATM.API.Models.Interfaces;

public interface IAtm
{
    public void Withdraw(int amount);
    public bool IsBalancePositive();
    public int GetTotalAmount();
}
