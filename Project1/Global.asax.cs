using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Project1.Managers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Project1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // внедрение зависимостей
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IStudentManager>().To<StudentManager>();
            Bind<ICaptainsManager>().To<CaptainsManager>();
            Bind<IGroupsManager>().To<GroupsManager>();
        }
    }

}
