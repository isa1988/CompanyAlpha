using Autofac;
using Autofac.Integration.Mvc;
using CompanyAlpha.Repository;
using CompanyAlpha.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebCompanyAlpha.App_Start;
using System.Web.Optimization;

namespace WebCompanyAlpha
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //AutoFac
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<DataProvider>().As<IDataProvider>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //Банглы
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
