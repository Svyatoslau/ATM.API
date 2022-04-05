using ATM.API.Services;

namespace ATM.API.Models;

public interface ISessional
{
    Guid StartSession(string cardNumber);
    void FinishSession(Guid token);
}

public sealed class Atm : IAtmService
{
    private readonly IBankService _bankService;
    private readonly ICardService _cardService;
    private readonly ISecurityManager _securityManager;

    private readonly CardSessionManager _cardSessionManager;
    
    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }

    private Guid Token { get; set; }
    private string CardNumber { get; set; }
    public Atm(IBankService bankService, ICardService cardService, ISecurityManager securityManager)
        => (_bankService, _cardService, _securityManager) = (bankService, cardService, securityManager);

    public void Withdraw(Guid token, int amount)
    {
        if (!_cardSessionManager.IsSessionAuthorized(token))
        {
            throw new UnauthorizedAccessException();
        }
        
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "You couldn't withdraw less or equal to zero.");
        }

        if (TotalAmount <= 0)
        {
            throw new InvalidOperationException
                ($"No cash available at the moment in the bank. Sorry.");
        }


        if (TotalAmount < amount)
        {
            throw new InvalidOperationException
                ($"You couldn't withdraw {amount}. Available ATM amount is {TotalAmount}. Sorry.");
        }

        var cardNumber = _cardSessionManager.GetCardNumber(token);

        _bankService.Withdraw(cardNumber, amount);

        TotalAmount -= amount;

        _cardSessionManager.FinishSession(token);
    }

    public void SaveCard(string cardNumber)
    {
        // I repeat this code in the Bank
        // Can I transfer it to the CardService?
        if (!_cardService.IsValidCardNumber(cardNumber))
        {
            throw new ArgumentOutOfRangeException(nameof(cardNumber), "Invalid card number.");
        }

        CardNumber = cardNumber;
        CleanToken();
    }

    public void ValidCard(string cardNumber, string password)
    {
        CardExist(cardNumber);
        VerifyPassword(password);
    }

    public void CardExist(string cardNumber)
    {
        if (!cardNumber.Equals(CardNumber))
        {
            throw new ArgumentException("Isn't valid card number", nameof(cardNumber));
        }
    }

    public void VerifyPassword(string password) 
        => _bankService.VerifyCardPassword(CardNumber, password);

    public Guid CreateToken()
    {
        Token = _securityManager.CreateToken();
        return Token;
    }

    public void ValidToken(string token)
    {
        if (Token == Guid.Empty)
        {
            throw new InvalidOperationException("Empty token.");
        }

        if (!token.Equals(Token.ToString()))
        {
            throw new ArgumentException("Unvalid token.");
        }
    }

    public void CleanToken() => Token = Guid.Empty;
    
    // New Methods
    public Guid StartSession(string cardNumber)
    {
        if (!_cardService.IsValidCardNumber(cardNumber))
        {
            throw new ArgumentException("Invalid card number.", nameof(cardNumber));
        }
        
        return _cardSessionManager.StartSession(cardNumber);
    }
    
    public bool Authorize(Guid token, string cardPassword)
    {
        if (!_cardSessionManager.IsSessionValid(token))
        {
            return false;
        }
        
        var cardNumber = _cardSessionManager.GetCardNumber(token);

        var card = _bankService.GetCard(cardNumber);

        if (!card.VerifyPassword(cardPassword))
        {
            return false;
        }
        
        _cardSessionManager.AuthorizeSession(token);

        return true;
    }
    
    public void FinishSession(Guid token) => _cardSessionManager.FinishSession(token);
    //
}

