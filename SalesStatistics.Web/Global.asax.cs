using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SalesStatistics.DAL.AutoMapperSetup;
using SalesStatistics.Web.AutoMapperSetup;

namespace SalesStatistics.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(x =>
            {
                x.AddProfile<AutoMapperDalProfile>();
                x.AddProfile<AutoMapperWebProfile>();
            });
        }
    }
}
