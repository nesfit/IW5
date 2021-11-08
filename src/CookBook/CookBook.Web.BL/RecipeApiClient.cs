using System.Net.Http;

namespace CookBook.Web.BL;

public partial class RecipeApiClient
{
    public RecipeApiClient(HttpClient httpClient, string baseUrl)
        : this(httpClient)
    {
            BaseUrl = baseUrl;
    }
}
