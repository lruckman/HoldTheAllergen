using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HoldTheAllergen.Backend.Core.Providers;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Backend.Core.Resolvers
{
    public class RestaurantIngredientAllergenListResolver : ValueResolver<RestaurantIngredient, MultiSelectList>
    {
        protected override MultiSelectList ResolveCore(RestaurantIngredient source)
        {
            return
                IoCHelper.GetInstance<AllergenMultiSelectListProvider>().Provide(
                    source.Allergens.Select(allergen => allergen.Id).ToArray());
        }
    }
}