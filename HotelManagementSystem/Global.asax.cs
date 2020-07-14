using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HotelManagementSystem.BLL.Infrastructure;
using HotelManagementSystem.DAL.Infrastructure;
using HotelManagementSystem.Utilities;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace HotelManagementSystem
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //dependency injection
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            NinjectModule roomModule = new RepositoryModule("DefaultConnection");
            var kernel = new StandardKernel(serviceModule, roomModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            Logger.InitLogger(); //Logger initialization
        }
    }
}