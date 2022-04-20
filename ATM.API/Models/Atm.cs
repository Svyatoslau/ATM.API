using ATM.API.Models.Interfaces;
using ATM.API.Services.Sessions;

namespace ATM.API.Models;

public sealed class Atm : IAtm
{
    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }

    public void Withdraw(int amount) => TotalAmount -= amount;

    public bool IsBalancePositive() => TotalAmount > 0;
}

