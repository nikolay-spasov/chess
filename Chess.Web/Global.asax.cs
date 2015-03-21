namespace Chess.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;

    using Chess.Infrastructure.Mapping;
    using Chess.Web.App_Start;
    using Chess.Web.Mapping;
    
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = SimpleInjectorConfig.Initialize();

            AutomapperInfrastructureConfiguration.Configure();
            AutomapperWebConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();

            container.Verify();
        }
    }
}
