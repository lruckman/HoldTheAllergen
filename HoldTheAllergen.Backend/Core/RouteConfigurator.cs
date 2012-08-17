using System.Web.Mvc;
using System.Web.Routing;
using AttributeRouting;

namespace HoldTheAllergen.Backend.Core
{
    public class RouteConfigurator
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Clear();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favIcon}", new {favIcon = @"(.*)favicon.ico$"});
            routes.IgnoreRoute("{*gifImages}", new {gifImages = @"(.*).gif$"});
            routes.IgnoreRoute("{*pngImages}", new {pngImages = @"(.*).png$"});
            routes.IgnoreRoute("{*jpgImages}", new {jpgImages = @"(.*).jpg$"});
            routes.IgnoreRoute("{*css}", new {css = @"(.*).css$"});
            routes.IgnoreRoute("{*js}", new {js = @"(.*).js$"});
            routes.IgnoreRoute("{*txt}", new {txt = @"(.*).txt$"});

            AreaRegistration.RegisterAllAreas();

            routes.MapAttributeRoutes();

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}