using AutoMapper;
using StructureMap.Configuration.DSL;

namespace HoldTheAllergen.Backend.Core.Dependency
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            For<IMappingEngine>().Use(() => Mapper.Engine);
        }
    }
}