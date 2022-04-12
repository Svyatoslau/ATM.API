namespace ATM.API.Middlewares;

public abstract class TimeSessionBase
{
    protected const string HeaderNameToken = "X-Token";
    
    private const int SessionExpirationTimeInMinutes = 1;
    
    protected static bool IsSessionExpired(DateTime createdAt) 
        => DateTime.UtcNow.AddMinutes(-SessionExpirationTimeInMinutes) > createdAt;

}
