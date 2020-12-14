using Newtonsoft.Json;

namespace CookBook.BL.Common.Services
{
    public class SerializerService : ISerializerService
    {
        private readonly JsonSerializerSettings defaultJsonSerializerSettings;

        public SerializerService()
        {
            defaultJsonSerializerSettings = new JsonSerializerSettings();
        }

        public string Serialize(object value, JsonSerializerSettings settings = null)
        {
            settings ??= defaultJsonSerializerSettings;
            return JsonConvert.SerializeObject(value, settings);
        }

        public T Deserialize<T>(string value, JsonSerializerSettings settings = null)
        {
            settings ??= defaultJsonSerializerSettings;
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }
}