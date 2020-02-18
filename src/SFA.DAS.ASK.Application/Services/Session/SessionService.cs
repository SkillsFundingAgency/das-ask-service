using Microsoft.AspNetCore.Http;

namespace SFA.DAS.ASK.Application.Services.Session
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string Get(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(key);
        }

        public void Set(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
        }
    }
}