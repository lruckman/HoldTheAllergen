using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using System.Web.Routing;
using HoldTheAllergen.API.App_Start;
using HoldTheAllergen.API.Core;
using StructureMap;

namespace HoldTheAllergen.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}