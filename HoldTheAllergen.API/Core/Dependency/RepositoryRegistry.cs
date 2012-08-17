using System.Configuration;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.DataAccess.Impl;
using HoldTheAllergen.Data.Models;
using StructureMap.Configuration.DSL;

namespace HoldTheAllergen.API.Core.Dependency
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For(typeof (IRepository<>)).HttpContextScoped().Use(typeof (Repository<>));

            For<HoldTheAllergenEntities>()
                .HttpContextScoped()
                .Use(
                    () =>
                    new HoldTheAllergenEntities(
                        ConfigurationManager.ConnectionStrings["HoldTheAllergenEntities"].ConnectionString));
        }
    }
}