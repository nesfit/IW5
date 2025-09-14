﻿using System.Globalization;
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
            var culture = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(culture))
            {
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
            }

            await next(context);
        }
    }
}