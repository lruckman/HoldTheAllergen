using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap.Configuration.DSL;

namespace HoldTheAllergen.API.Core.Dependency
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry()
        {
            For<IPrincipal>().HttpContextScoped().Use(() => HttpContext.Current.User);
            For<ITempDataProvider>().Use<SessionStateTempDataProvider>();
            For<RouteCollection>().Use(RouteTable.Routes);
        }
    }
}