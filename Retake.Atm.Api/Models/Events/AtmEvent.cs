namespace Retake.Atm.Api.Models.Events;

public abstract record AtmEventBase
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}