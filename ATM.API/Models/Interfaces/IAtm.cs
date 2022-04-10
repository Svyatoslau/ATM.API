using ATM.API.Models;

namespace ATM.API.Models.Interfaces;

public interface IAtm
{
    void Withdraw(Guid token, int amount);
}
