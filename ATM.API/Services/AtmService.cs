﻿using ATM.API.Models.Interfaces;
using ATM.API.Services.Interfaces;
using ATM.API.Services.Sessions;

namespace ATM.API.Services;

public class AtmService : IAtmService
{
    private readonly IAtm _atm;
    private readonly IBank _bank;
    private readonly ICardSecurity _cardSecurity;
    private readonly SessionManager _sessionManager;
    private readonly ICardService _cardService;

    public AtmService(IAtm atm, IBank bank, ICardSecurity cardSecurity, SessionManager sessionManager, ICardService cardService)
        => (_atm, _bank, _cardSecurity, _sessionManager, _cardService) = (atm, bank, cardSecurity, sessionManager, cardService);
    public void AuthorizeSession(Guid token, string password)
    {
        var cardNumber = _sessionManager.GetCardNumber(token);
        _cardSecurity.VerifyCardPassword(cardNumber, password);

        _sessionManager.Authorize(token);
    }

    public void FinishSession(Guid token)
    {
        AuthorizedSession(token);

        _sessionManager.FinishSession(token);
    }
    public Guid StartSession(string cardNumber)
    {
        if (!_cardService.IsValidCardNumber(cardNumber))
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
        AuthorizedSession(token);

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

        _sessionManager.FinishSession(token);
    }

    public int GetCardBalance(Guid token)
    {
        AuthorizedSession(token);

        var cardNumber = _sessionManager.GetCardNumber(token);


        //_sessionManager.FinishSession(token);

        // Finish session after check balance?
        // Or user can continue?

        return _bank.GetCardBalance(cardNumber);
    }

    private void AuthorizedSession(Guid token)
    {
        if (!_sessionManager.IsAuthorized(token))
        {
            throw new UnauthorizedAccessException("Unauthorized session.");
        } 
    }
}
