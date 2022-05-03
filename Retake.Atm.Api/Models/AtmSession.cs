namespace Retake.Atm.Api.Models;

public sealed record AtmSession(string CardNumber)
{
    public Guid Id { get; } = Guid.NewGuid();
    public bool IsAuthorized { get; init; } = false;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}