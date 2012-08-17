using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HoldTheAllergen.Backend.Core.Providers;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Backend.Core.Resolvers
{
    public class MenuItemIngredientListResolver : ValueResolver<MenuItem, MultiSelectList>
    {
        protected override MultiSelectList ResolveCore(MenuItem source)
        {
            return IoCHelper.GetInstance<RestaurantIngredientMultiSelectListProvider>()
                .Provide(source.RestaurantId, source.Ingredients.Select(ingredient => ingredient.Id));
        }
    }
}