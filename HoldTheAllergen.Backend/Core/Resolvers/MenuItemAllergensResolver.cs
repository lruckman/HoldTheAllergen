using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Backend.Core.Resolvers
{
    public class MenuItemAllergensResolver : ValueResolver<MenuItem, IEnumerable<string>>
    {
        protected override IEnumerable<string> ResolveCore(MenuItem source)
        {
            return
                source.Ingredients
                    .SelectMany(ingredient => ingredient.Allergens.Select(a => a.Name))
                    .Distinct()
                    .OrderBy(allergen => allergen);
        }
    }
}