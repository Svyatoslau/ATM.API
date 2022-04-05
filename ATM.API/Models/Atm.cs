using ATM.API.Services;

namespace ATM.API.Models;

public sealed class Atm : IAtmService
{
    private readonly IBankService _bankService;
    private readonly ICardService _cardService;
    private readonly ISecurityManager _securityManager;
    private int TotalAmount { get; set; } = 1000;
    public int GetTotalAmount() { return TotalAmount; }

    private Guid Token { get; set; }
    private string CardNumber { get; set; }
    public Atm(IBankService bankService, ICardService cardService, ISecurityManager securityManager)
        => (_bankService, _cardService, _securityManager) = (bankService, cardService, securityManager);

    public void Withdraw(int amount)
    {
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

        _bankService.Withdraw(CardNumber, amount);

        TotalAmount -= amount;

        CleanToken();
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
    
}

