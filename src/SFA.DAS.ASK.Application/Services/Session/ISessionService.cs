namespace SFA.DAS.ASK.Application.Services.Session
{
    public interface ISessionService
    {
        string Get(string key);
        T Get<T>(string key);
        void Set(string key, string value);
        void Set<T>(string key, T value);

        void Remove(string key);
    }
}