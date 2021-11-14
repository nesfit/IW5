using System.Net.Http;

namespace CookBook.Maui.BL.Api;

public partial class RecipeApiClient
{
    public RecipeApiClient(HttpClient httpClient, string baseUrl)
        : this(httpClient)
    {
            BaseUrl = baseUrl;
    }
}
