using CookBook.App.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CookBook.App.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}