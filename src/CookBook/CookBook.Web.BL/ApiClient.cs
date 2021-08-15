using System.Net.Http;

namespace CookBook.Web.BL
{
    public partial class ApiClient
    {
        public ApiClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
