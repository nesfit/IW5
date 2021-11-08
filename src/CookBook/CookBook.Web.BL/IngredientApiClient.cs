using System.Net.Http;

namespace CookBook.Web.BL
{
    public partial class IngredientApiClient
    {
        public IngredientApiClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
