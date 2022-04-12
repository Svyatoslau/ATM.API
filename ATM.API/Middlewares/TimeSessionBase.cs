namespace ATM.API.Middlewares;

public abstract class TimeSessionBase
{
    protected const string HeaderNameToken = "X-Token";

    protected const int SessionExpirationTimeInMinutes = 1;

    protected bool IsSessionExpired(DateTime createdAt)
        => DateTime.UtcNow.AddMinutes(-SessionExpirationTimeInMinutes) > createdAt;
}
