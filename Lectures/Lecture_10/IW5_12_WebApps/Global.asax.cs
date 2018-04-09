using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IW5_12_WebApps
{
    public class MvcApplication : System.Web.HttpApplication
    {

        // The main entry point for Asp .Net application
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas(); // Not used in our example, for large applicaitons.
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
