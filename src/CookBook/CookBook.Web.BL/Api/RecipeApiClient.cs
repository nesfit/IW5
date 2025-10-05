namespace CookBook.Web.BL.Api;

public partial class RecipeApiClient
{
    public RecipeApiClient(HttpClient httpClient, string baseUrl)
        : this(httpClient)
    {
        BaseUrl = baseUrl;
    }
}
