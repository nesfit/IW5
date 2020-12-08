using Newtonsoft.Json;

namespace CookBook.BL.Common.Services
{
    public interface ISerializerService : IAppService
    {
        string Serialize(object value, JsonSerializerSettings settings = null);
        T Deserialize<T>(string value, JsonSerializerSettings settings = null);
    }
}