namespace ATM.API.Models;

public sealed record CardSessionModel(string CardNumber, Guid Token, DateTime CreationTime)
{
    public bool IsAuthorized { get; init; } = false;
}
