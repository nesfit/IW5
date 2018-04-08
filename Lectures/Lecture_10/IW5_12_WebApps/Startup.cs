using Microsoft.Owin;
using Owin;

// Tel owin what to do during startup
[assembly: OwinStartupAttribute(typeof(IW5_12_WebApps.Startup))]

namespace IW5_12_WebApps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
