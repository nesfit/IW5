using CookBook.Api.App.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CookBook.Api.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}