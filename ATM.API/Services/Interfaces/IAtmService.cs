using ATM.API.Models;
using ATM.API.Models.API;
using ATM.API.Models.Interfaces;
using ATM.API.Services.Interfaces.Atm;

namespace ATM.API.Services.Interfaces;

public interface IAtmService : IReceipt, ISessionAtm, IMoneyOperations
{

}
