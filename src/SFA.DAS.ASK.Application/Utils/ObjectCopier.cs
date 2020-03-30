using Newtonsoft.Json;

namespace SFA.DAS.ASK.Application.Utils
{
    public static class ObjectCopier
    {
        public static T CloneJson<T>(this T source)
        {            
            if (ReferenceEquals(source, null))
            {
                return default;
            }

            var deserializeSettings = new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace};

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}