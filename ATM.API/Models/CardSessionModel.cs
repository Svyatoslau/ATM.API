namespace ATM.API.Models;

public sealed record CardSessionModel(string CardNumber, Guid Token)
{
    public bool IsAuthorized { get; init; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public bool Receipt { get; init; }

}
