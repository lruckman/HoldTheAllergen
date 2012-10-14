using System.Data.Entity;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.DataAccess.Impl;
using HoldTheAllergen.Data.Models;
using StructureMap.Configuration.DSL;

namespace HoldTheAllergen.Backend.Core.Dependency
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For(typeof (IRepository<>)).HttpContextScoped().Use(typeof (Repository<>));

            For<DbContext>()
                .HttpContextScoped()
                .Use(() => new HoldTheAllergenEntities());
        }
    }
}