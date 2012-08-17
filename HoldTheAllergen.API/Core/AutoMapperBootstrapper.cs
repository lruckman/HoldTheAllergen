using AutoMapper;

namespace HoldTheAllergen.API.Core
{
    public static class AutoMapperBootstrapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ModelProfile>());
        }

        #region Nested type: ModelProfile

        public class ModelProfile : Profile
        {
            protected override void Configure()
            {
            }
        }

        #endregion
    }
}