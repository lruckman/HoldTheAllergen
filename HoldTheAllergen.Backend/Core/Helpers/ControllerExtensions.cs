using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;

namespace HoldTheAllergen.Backend.Core.Helpers
{
    public static class ControllerExtensions
    {
        public static RedirectToRouteResult RedirectToAction<T>(this T controller, Expression<Action<T>> action)
            where T : Controller
        {
            return RedirectToActionInternal(action, null);
        }

        public static RedirectToRouteResult RedirectToAction<T>(this Controller controller, Expression<Action<T>> action)
            where T : Controller
        {
            return RedirectToActionInternal(action, null);
        }

        public static RedirectToRouteResult RedirectToAction<T>(this T controller, Expression<Action<T>> action,
                                                                object values)
            where T : Controller
        {
            return RedirectToActionInternal(action, new RouteValueDictionary(values));
        }

        public static RedirectToRouteResult RedirectToAction<T>(this Controller controller, Expression<Action<T>> action,
                                                                object values)
            where T : Controller
        {
            return RedirectToActionInternal(action, new RouteValueDictionary(values));
        }

        public static RedirectToRouteResult RedirectToAction<T>(this T controller, Expression<Action<T>> action,
                                                                RouteValueDictionary values)
            where T : Controller
        {
            return RedirectToActionInternal(action, values);
        }

        public static RedirectToRouteResult RedirectToAction<T>(this Controller controller, Expression<Action<T>> action,
                                                                RouteValueDictionary values)
            where T : Controller
        {
            return RedirectToActionInternal(action, values);
        }

        private static RedirectToRouteResult RedirectToActionInternal<T>(Expression<Action<T>> action,
                                                                         RouteValueDictionary values)
            where T : Controller
        {
            var body = action.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("Expression must be a method call.");
            }

            if (body.Object != action.Parameters[0])
            {
                throw new ArgumentException("Method call must target lambda argument.");
            }

            var actionName = body.Method.Name;

            var attributes = body.Method.GetCustomAttributes(typeof (ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute) attributes[0];
                actionName = actionNameAttr.Name;
            }

            var controllerName = typeof (T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }

            var defaults = LinkBuilder.BuildParameterValuesFromExpression(body) ?? new RouteValueDictionary();

            values = values ?? new RouteValueDictionary();
            values.Add("controller", controllerName);
            values.Add("action", actionName);

            foreach (var pair in defaults.Where(p => p.Value != null))
            {
                values.Add(pair.Key, pair.Value);
            }

            return new RedirectToRouteResult(values);
        }

        public static string RouteUrl(this Controller controller, string routeName)
        {
            return RouteUrl(controller, routeName, null);
        }

        public static string RouteUrl(this Controller controller, string routeName, object routeValues)
        {
            return controller.Url.RouteUrl(routeName, routeValues);
        }
    }
}