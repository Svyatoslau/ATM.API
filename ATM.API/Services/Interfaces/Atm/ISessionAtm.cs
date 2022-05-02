namespace ATM.API.Services.Interfaces.Atm;

public interface ISessionAtm
{
    public Guid StartSession(string cardNumber);
    public void AuthorizeSession(Guid token, string password);
    public void FinishSession(Guid token);
}
