using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HoldTheAllergen.Backend.Core;
using HoldTheAllergen.Backend.Core.Dependency;
using HoldTheAllergen.Backend.Core.Helpers;
using HoldTheAllergen.Backend.Core.Providers;

namespace HoldTheAllergen.Backend
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            var container = IoCBootstrapper.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfigurator.RegisterRoutes(RouteTable.Routes);

            AutoMapperBootstrapper.Initialize();
            BundleTable.Bundles.RegisterTemplateBundles();
            BundleTable.Bundles.EnableBootstrapBundle();
            ModelMetadataProviders.Current = new MetadataConventionProvider();
        }
    }
}