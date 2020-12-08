using CookBook.BL.Web.MVC.Options;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace CookBook.BL.Web.MVC.Api
{
    public partial class RecipeClient
    {
        public RecipeClient(IOptions<ApiOptions> apiOptions, HttpClient httpClient)
            : this(httpClient)
        {
            BaseUrl = apiOptions.Value.BaseUrl;
        }
    }
}
