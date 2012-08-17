using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using System.Web.Routing;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.API.Core.Dependency;

namespace HoldTheAllergen.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                "DefaultUserApi",
                "{user}/{controller}/{id}",
                new {id = RouteParameter.Optional},
                new { user = new GuidConstraint() }
                );

            routes.MapHttpRoute(
                "RestaurantMenu",
                "{user}/restaurant/{restaurantId}/menu",
                new { controller = "restaurantmenu" },
                new { user = new GuidConstraint() }
                );

            routes.MapHttpRoute(
                "RestaurantStarredMenu",
                "{user}/restaurant/{restaurantId}/menu/starred",
                new { controller = "starredrestaurantmenu" },
                new { user = new GuidConstraint() }
                );

            routes.MapHttpRoute(
                "StarMenuItem",
                "{user}/menuitem/{id}/star",
                new { controller = "starmenuitem" },
                new { user = new GuidConstraint() }
                );

            routes.MapHttpRoute(
                "MenuItemIngredients",
                "{user}/menuitem/{id}/ingredients",
                new { controller = "menuitemingredients" },
                new { user = new GuidConstraint() }
                );
        }

        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();

            var container = IoCBootstrapper.Initialize();
            var config = GlobalConfiguration.Configuration;

            config.ServiceResolver.SetResolver(new StructureMapDependencyResolver(container));

            var modelBinderProviderServices = config.ServiceResolver.GetServices(typeof(ModelBinderProvider));
            var services = new List<object>(modelBinderProviderServices) {new CustomModelBinderProvider()};
            config.ServiceResolver.SetServices(typeof(ModelBinderProvider), services.ToArray());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AutoMapperBootstrapper.Initialize();
        }
    }
}