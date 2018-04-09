using System.Web.Mvc;

namespace IW5_12_WebApps
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Adds default error handling see also Views\Shared\Error.cshtml
            filters.Add(new HandleErrorAttribute());
        }
    }
}
