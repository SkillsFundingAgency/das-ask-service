namespace SFA.DAS.ASK.Application.Services.Session
{
    public interface ISessionService
    {
        string Get(string key);
        void Set(string key, string value);
    }
}