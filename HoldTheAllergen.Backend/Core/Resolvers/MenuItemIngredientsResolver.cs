using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Backend.Core.Resolvers
{
    public class MenuItemIngredientsResolver : ValueResolver<MenuItem, IEnumerable<string>>
    {
        protected override IEnumerable<string> ResolveCore(MenuItem source)
        {
            return source.Ingredients.Select(ingredient => ingredient.Name).OrderBy(ingredient => ingredient).ToArray();
        }
    }
}