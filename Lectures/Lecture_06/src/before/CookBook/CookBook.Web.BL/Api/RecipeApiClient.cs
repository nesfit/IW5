using System.Text.Json;

namespace CookBook.Web.BL.Api;

public partial class RecipeApiClient
{
    static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
    {
        settings.PropertyNameCaseInsensitive = true;
        settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }
}
