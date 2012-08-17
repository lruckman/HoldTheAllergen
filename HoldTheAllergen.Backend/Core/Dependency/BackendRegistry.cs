using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap.Configuration.DSL;

namespace HoldTheAllergen.Backend.Core.Dependency
{
    public class BackendRegistry : Registry
    {
        public BackendRegistry()
        {
            For<IPrincipal>().HttpContextScoped().Use(() => HttpContext.Current.User);
            For<IActionInvoker>().Use<InjectingActionInvoker>();
            For<ITempDataProvider>().Use<SessionStateTempDataProvider>();
            For<RouteCollection>().Use(RouteTable.Routes);

            SetAllProperties(c =>
            {
                c.OfType<IActionInvoker>();
                c.OfType<ITempDataProvider>();
            });
        }
    }
}