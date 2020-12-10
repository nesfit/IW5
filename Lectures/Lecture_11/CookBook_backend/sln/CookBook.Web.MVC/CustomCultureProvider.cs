using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Threading.Tasks;

namespace CookBook.Web.MVC
{
    public class CustomCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments(new PathString("/cs")))
            {
                return new ProviderCultureResult("cs");
            }
            else if (httpContext.Request.Path.StartsWithSegments(new PathString("/en")))
            {
                return new ProviderCultureResult("en");
            }
            else
            {
                return null;
            }
        }
    }
}