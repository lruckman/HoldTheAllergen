using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;
using StructureMap;

namespace HoldTheAllergen.Backend.Core.Dependency
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
                        scanner.WithDefaultConventions();
                        scanner.ConnectImplementationsToTypesClosing(typeof (IRepository<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof(IActionMethodResultInvoker<>));
                        scanner.ConnectImplementationsToTypesClosing(typeof (IFormHandler<>));
                        scanner.Convention<StructureMapCommandMessageConvention>();
                    }));
        }
    }
}