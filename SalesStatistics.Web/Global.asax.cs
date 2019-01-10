using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SalesStatistics.DAL;
using SalesStatistics.DAL.AutoMapperSetup;
using SalesStatistics.DAL.Contracts.DTO;
using SalesStatistics.Web.Models.ViewModels;

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
                x.CreateMap<ManagerViewModel, ManagerDTO>();
                x.CreateMap<ManagerDTO, ManagerViewModel>();

                // Profile with mapping entities to DTO and vice versa
                x.AddProfile<AutoMapperProfile>();
            });
        }
    }
}
