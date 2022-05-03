using Retake.Atm.Api.Models.Events;
using Retake.Atm.Api.Interfaces.Services;

namespace Retake.Atm.Api.Services;

public sealed class AtmService : IAtmService
{
    private readonly IAtmEventBroker _eventBroker;
    private readonly IAtmOperationService _atmOperation;

    public AtmService(IAtmEventBroker eventBroker, IAtmOperationService atmOperation)
    {
        _eventBroker = eventBroker;
        _atmOperation = atmOperation;
    }

    public void Init(string cardNumber)
    {
        if (!_atmOperation.HasCard(cardNumber))
        {
            throw new InvalidOperationException();
        }
        
        _eventBroker.StartStream(cardNumber, new CardInitialized());
    }
    
    public void Authorize(string cardNumber, string cardPassword)
    {
        var @event = _eventBroker.GetLastEvent(cardNumber);

        if (@event is not CardInitialized)
        {
            throw new InvalidOperationException();
        }

        if (!_atmOperation.VerifyPassword(cardNumber, cardPassword))
        {
            throw new InvalidOperationException();
        }

        _eventBroker.AppendEvent(cardNumber, new CardAuthorized());
    }

    public int GetBalance(string cardNumber)
    {
        var @event = _eventBroker.GetLastEvent(cardNumber);

        if (@event is not CardAuthorized)
        {
            throw new InvalidOperationException("Could not perform unauthorized operation");
        }

        return _atmOperation.GetBalance(cardNumber);
    }
    
    public void Withdraw(string cardNumber, int amount)
    {
        var @event = _eventBroker.GetLastEvent(cardNumber);

        if (@event is not CardAuthorized)
        {
            throw new InvalidOperationException("Could not perform unauthorized operation");
        }
        
        _atmOperation.Withdraw(cardNumber, amount);

        _eventBroker.AppendEvent(cardNumber, new MoneyWithdrew(amount));
    }
}