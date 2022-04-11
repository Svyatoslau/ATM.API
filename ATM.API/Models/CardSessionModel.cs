namespace ATM.API.Models;

public sealed record CardSessionModel(string CardNumber, Guid Token)
{
    public bool IsAuthorized { get; init; } = false;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
