using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.DataAccess.Impl;
using HoldTheAllergen.Data.Models;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace HoldTheAllergen.API.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.LookForRegistries();
                    scan.Assembly("HoldTheAllergen.Data");
                    scan.Assembly("HoldTheAllergen.Models");
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.ConnectImplementationsToTypesClosing(
                        typeof (IRepository<>));
                }));


            return ObjectFactory.Container;
        }
    }

    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For(typeof (IRepository<>)).Use(typeof (Repository<>));

            For<DbContext>()
                .HybridHttpOrThreadLocalScoped()
                .Use(() => new HoldTheAllergenEntities());
        }
    }

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