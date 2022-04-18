using ATM.API.Models.Interfaces;

namespace ATM.API.Services.Interfaces;

public interface IAtmService : ISessional 
{
    public void WithdrawMoney(Guid token, int amount);
}
