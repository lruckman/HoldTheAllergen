using System.Web.Http;
using HoldTheAllergen.API.Core;

namespace HoldTheAllergen.API.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultUserApi",
                "{userId}/{controller}/{id}",
                new {id = RouteParameter.Optional},
                new { userId = new GuidConstraint() }
                );

            config.Routes.MapHttpRoute(
                "RestaurantMenu",
                "{userId}/restaurant/{restaurantId}/menu",
                new {controller = "restaurantmenu"},
                new { userId = new GuidConstraint() }
                );

            config.Routes.MapHttpRoute(
                "RestaurantStarredMenu",
                "{userId}/restaurant/{restaurantId}/menu/starred",
                new {controller = "starredrestaurantmenu"},
                new { userId = new GuidConstraint() }
                );

            config.Routes.MapHttpRoute(
                "RestaurantAllergyFreeMenu",
                "{userId}/restaurant/{restaurantId}/menu/allergyfree",
                new {controller = "allergyfreerestaurantmenu"},
                new { userId = new GuidConstraint() }
                );

            config.Routes.MapHttpRoute(
                "StarMenuItem",
                "{userId}/menuitem/{id}/star",
                new {controller = "starmenuitem"},
                new { userId = new GuidConstraint() }
                );

            config.Routes.MapHttpRoute(
                "MenuItemIngredients",
                "{userId}/menuitem/{id}/ingredients",
                new {controller = "menuitemingredients"},
                new { userId = new GuidConstraint() }
                );
        }
    }
}