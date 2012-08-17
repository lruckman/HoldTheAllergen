using HoldTheAllergen.Data.DataAccess;
using StructureMap;

namespace HoldTheAllergen.API.Core.Dependency
{
    public class DependencyRegistrar
    {
        public static void Initialize()
        {
            ObjectFactory.Initialize(cfg => cfg.Scan(scanner =>
                                                         {
                                                             scanner.TheCallingAssembly();
                                                             
                                                             scanner.LookForRegistries();
                                                             scanner.Assembly("HoldTheAllergen.Data");
                                                             scanner.Assembly("HoldTheAllergen.Models");
                                                             scanner.TheCallingAssembly();
                                                             scanner.WithDefaultConventions();
                                                             scanner.ConnectImplementationsToTypesClosing(
                                                                 typeof (IRepository<>));
                                                         }));
        }
    }
}