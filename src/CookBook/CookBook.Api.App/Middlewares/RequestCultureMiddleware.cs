using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CookBook.Api.App.Middlewares
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Query.TryGetValue("culture", out var cultureValues))
            {
                var culture = cultureValues.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(culture))
                {
                    var cultureInfo = new CultureInfo(culture);
                    CultureInfo.CurrentCulture = cultureInfo;
                    CultureInfo.CurrentUICulture = cultureInfo;
                }
            }

            await next(context);
        }
    }
}
