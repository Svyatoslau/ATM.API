namespace ATM.API.Services.Interfaces;


public interface ISessional
{
    Guid StartSession(string cardNumber);
    void FinishSession(Guid token);
    public void AuthorizeSession(Guid token, string password);
}