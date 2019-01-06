using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesStatistics.Web.Startup))]
namespace SalesStatistics.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
