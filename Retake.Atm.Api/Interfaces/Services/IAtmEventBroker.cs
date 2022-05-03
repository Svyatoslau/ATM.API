using Retake.Atm.Api.Models.Events;

namespace Retake.Atm.Api.Interfaces.Services;

public interface IAtmEventBroker
{
    void StartStream(string key, AtmEventBase @event);
    void AppendEvent(string key, AtmEventBase @event);
    AtmEventBase GetLastEvent(string key);
}