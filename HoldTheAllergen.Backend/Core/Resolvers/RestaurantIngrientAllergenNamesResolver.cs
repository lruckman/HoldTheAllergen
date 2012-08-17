using System.Linq;
using AutoMapper;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Backend.Core.Resolvers
{
    public class RestaurantIngredientAllergenNamesResolver :
        ValueResolver<RestaurantIngredient, string>
    {
        protected override string ResolveCore(RestaurantIngredient source)
        {
            return string.Join(", ", source.Allergens.Select(a => a.Name).Distinct().OrderBy(allergen => allergen));
        }
    }
}