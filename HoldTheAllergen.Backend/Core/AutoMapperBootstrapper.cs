using System.Linq;
using AutoMapper;
using HoldTheAllergen.Backend.Core.Resolvers;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models;

namespace HoldTheAllergen.Backend.Core
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
                /* MenuItem */
                CreateMap<MenuItem, MenuItemModel>()
                    .ForMember(d => d.Allergens, o => o.ResolveUsing<MenuItemAllergensResolver>())
                    .ForMember(d => d.Ingredients, o => o.ResolveUsing<MenuItemIngredientsResolver>());
                CreateMap<MenuItem, MenuItemCreateModel>()
                    .ForMember(d => d.IngredientIds, o => o.Ignore())
                    .ForMember(d => d.IngredientList, o => o.ResolveUsing<MenuItemIngredientListResolver>());

                CreateMap<MenuItem, MenuItemEditModel>()
                    .ForMember(d => d.IngredientIds, o => o.MapFrom(s=>s.Ingredients.Select(x=>x.Id)))
                    .ForMember(d => d.IngredientList, o => o.ResolveUsing<MenuItemIngredientListResolver>());

                CreateMap<MenuItem, MenuItemDeleteModel>();

                /* Restaurant */
                CreateMap<Restaurant, RestaurantModel>();

                CreateMap<Restaurant, RestaurantCreateModel>()
                    .ForMember(d => d.Logo, o => o.Ignore());

                CreateMap<Restaurant, RestaurantDeleteModel>();

                /* RestaurantIngredient */
                CreateMap<RestaurantIngredient, RestaurantIngredientModel>()
                    .ForMember(d => d.AllergenNames, o => o.ResolveUsing<RestaurantIngredientAllergenNamesResolver>());

                CreateMap<RestaurantIngredient, RestaurantIngredientCreateModel>()
                    .ForMember(d => d.AllergenIds, o => o.Ignore())
                    .ForMember(d => d.AllergenList, o => o.ResolveUsing<RestaurantIngredientAllergenListResolver>())
                    .ForMember(d => d.CreateMenuItem, o => o.Ignore())
                    .ForMember(d => d.GroupName, o => o.Ignore());

                CreateMap<RestaurantIngredient, RestaurantIngredientDeleteModel>();

                CreateMap<RestaurantIngredient, RestaurantIngredientEditModel>()
                    .ForMember(d => d.AllergenIds, o => o.MapFrom(s => s.Allergens.Select(allergen => allergen.Id)))
                    .ForMember(d => d.AllergenList, o => o.ResolveUsing<RestaurantIngredientAllergenListResolver>());

                Mapper.AssertConfigurationIsValid();
            }
        }

        #endregion
    }
}