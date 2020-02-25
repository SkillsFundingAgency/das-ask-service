using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        public void Set<T>(string key, T value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }

        public T Get<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(_httpContextAccessor.HttpContext.Session.GetString(key));
        }
    }
}