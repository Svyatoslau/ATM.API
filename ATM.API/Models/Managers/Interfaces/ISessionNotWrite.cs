namespace ATM.API.Models.Managers.Interfaces;

public interface ISessionNotWrite
{
    public CardSessionModel GetSession(Guid token);
    public void FinishSession(Guid token);
}
