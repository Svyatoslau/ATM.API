using ATM.API.Models;
using ATM.API.Models.API;
using ATM.API.Models.Interfaces;
using ATM.API.Services.Interfaces;
using ATM.API.Services.Sessions;
using System.Text.RegularExpressions;

namespace ATM.API.Services;

public class AtmService : IAtmService
{
    private readonly IAtm _atm;
    private readonly IBank _bank;
    private readonly SessionManager _sessionManager;

    //It looks like you have here a lot of dependencies
    public AtmService(IAtm atm, IBank bank, SessionManager sessionManager)
        => (_atm, _bank, _sessionManager) = (atm, bank, sessionManager);
    public void AuthorizeSession(Guid token, string password)
    {
        var cardNumber = _sessionManager.GetCardNumber(token);
        _bank.VerifyCardPassword(cardNumber, password);

        _sessionManager.Authorize(token);
    }

    public void FinishSession(Guid token)
    {
        _sessionManager.Finish(token);
    }
    public Guid StartSession(string cardNumber)
    {
        ;
        if (!Regex.IsMatch(cardNumber, @"\d{16}"))
        {
            throw new ArgumentOutOfRangeException(nameof(cardNumber), "Invalid card number.");
        }

        if (!_bank.CardExist(cardNumber))
        {
            throw new ArgumentNullException(nameof(cardNumber), "Not existing card.");
        }

        return _sessionManager.Start(cardNumber);
    }

    public void WithdrawMoney(Guid token, int amount)
    {

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "You couldn't withdraw less or equal to zero.");
        }

        if (!_atm.IsBalancePositive())
        {
            throw new InvalidOperationException
                ($"No cash available at the moment in the bank. Sorry.");
        }

        if (_atm.GetTotalAmount() < amount)
        {
            throw new InvalidOperationException
                ($"You couldn't withdraw {amount}. Available ATM amount is {_atm.GetTotalAmount()}. Sorry.");
        }

        var cardNumber = _sessionManager.GetCardNumber(token);

        _bank.Withdraw(cardNumber, amount);
        _atm.Withdraw(amount);
    }

    public int GetCardBalance(Guid token)
    {
        var cardNumber = _sessionManager.GetCardNumber(token);

        return _bank.GetCardBalance(cardNumber);
    }

    public void IsIncludeReceipt(Guid token, bool answer)
    {
        _sessionManager.Receipt(token, answer); 
    }

    public Receipt GetReceipt(Guid token, int amount)
    {
        if (!_sessionManager.IsIncludeReceipt(token))
        {
            return null;
        }

        var cardNumber = _sessionManager.GetCardNumber(token);

        return new Receipt($"**** **** **** {cardNumber[12..]}", amount);
    }
}
