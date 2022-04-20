using ATM.API.Models;
using ATM.API.Models.API;
using ATM.API.Models.Interfaces;

namespace ATM.API.Services.Interfaces;

public interface IAtmService : ISessional 
{
    public void WithdrawMoney(Guid token, int amount);
    public int GetCardBalance(Guid token);
    public void Receipt(Guid token, string answer);
    public bool IsIncludeReceipt(Guid token);
    public Receipt GetWithdrawReceipt(Guid token, int amount);
}
