using ATM.API.Models;
using ATM.API.Repositories;

namespace ATM.API.Services;

public sealed class ReceiptService
{
    private readonly SessionStorage _sessionStorage;

    public ReceiptService(SessionStorage sessionStorage) => _sessionStorage = sessionStorage;

    // To reduce this weird logic 
    // Use bool at the beginning
    public bool ParseAnswerToBool(string answer) => answer.ToLower() switch
    {
        "yes" => true,
        "no" => false,
        _ => throw new ArgumentOutOfRangeException(nameof(answer),
            $"Invalid parameter.Available parameters are: \"yes\", \"no\""),
    };

    // It is better to take cardNumber.Length and 4 last digits
    // Then make others *
    public Receipt WithdrawReceipt(string cardNumber, int amount)
        => new Receipt($"**** **** **** {cardNumber[12..]}", amount);

    public void Receipt(Guid token, string answer)
    {
        var condition = ParseAnswerToBool(answer);

        var session = _sessionStorage.Find(token);

        _sessionStorage.Remove(session);

        _sessionStorage.Add(session with { Receipt = condition });
    }

    public bool IsInclude(Guid token) => _sessionStorage.Find(token).Receipt;
}
