using ATM.API.Models;

namespace ATM.API.Services.Interfaces.Atm;

public interface IReceipt
{
    public Receipt GetReceipt(Guid token, int amount);
    public void IsIncludeReceipt(Guid token, bool answer);
}
