namespace CookBook.Web.BL.Api;

public partial class IngredientApiClient
{
    public IngredientApiClient(HttpClient httpClient, string baseUrl)
        : this(httpClient)
    {
        BaseUrl = baseUrl;
    }
}
