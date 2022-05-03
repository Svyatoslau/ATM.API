using Retake.Atm.Api.Models.Events;
using Retake.Atm.Api.Interfaces.Services;

namespace Retake.Atm.Api.Services;

public sealed class AtmEventBroker : IAtmEventBroker
{
    private readonly IDictionary<string, ICollection<AtmEventBase>> _events 
        = new Dictionary<string, ICollection<AtmEventBase>>();

    public void StartStream(string key, AtmEventBase @event)
    {
        if (_events.ContainsKey(key))
        {
            _events.Remove(key);
        }
        
        _events[key] = new List<AtmEventBase> { @event };
    }
    
    public void AppendEvent(string key, AtmEventBase @event)
    {
        if (!_events.TryGetValue(key, out var events))
        {
            throw new KeyNotFoundException();
        }
        
        events.Add(@event);
    }

    public AtmEventBase GetLastEvent(string key)
    {
        if (_events.TryGetValue(key, out var events))
        {
            return events.Last();
        }
        
        throw new KeyNotFoundException();
    }
}