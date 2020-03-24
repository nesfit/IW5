using CookBook.BL.Web.Options;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace CookBook.BL.Web.Api
{
    public partial class IngredientClient
    {
        public IngredientClient(IOptions<ApiOptions> apiOptions, HttpClient httpClient)
            : this(httpClient)
        {
            //BaseUrl = apiOptions.Value.BaseUrl;
        }
    }
}
