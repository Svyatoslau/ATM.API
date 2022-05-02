namespace ATM.API.Services.Interfaces;


public interface ISessional
{
    Guid Start(string cardNumber);
    void Finish(Guid token);
    public void Authorize(Guid token);
}