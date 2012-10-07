using System.Collections.Generic;
using System.Linq;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Core
{
    public class MenuItemIngredientsModelMapper
    {
        public static IList<KeyValuePair<string, IList<IngredientItemModel>>> Map(MenuItem menuItem, User user)
        {
            var allergensToAvoid = user.Allergies.Select(allergen => allergen.Id).ToArray();
            var res = new List<KeyValuePair<string, IList<IngredientItemModel>>>();

            var ingredients = menuItem.Ingredients
                .Where(ingredient => !ingredient.Allergens.Any(allergen => allergensToAvoid.Contains(allergen.Id)))
                .OrderBy(ingredient => ingredient.Name)
                .Select(ingredient => new IngredientItemModel
                    {
                        AllergenNames = string.Empty,
                        Name = ingredient.Name,
                        Description = ingredient.Description ?? ""
                    })
                .ToList();

            res.Add(
                new KeyValuePair<string, IList<IngredientItemModel>>(
                    string.Format("{0} Items you can have", ingredients.Count), ingredients));

            ingredients = menuItem.Ingredients
                .Where(ingredient => ingredient.Allergens.Any(allergen => allergensToAvoid.Contains(allergen.Id)))
                .OrderBy(ingredient => ingredient.Name)
                .Select(ingredient => new IngredientItemModel
                    {
                        AllergenNames = string.Join(", ", ingredient.Allergens
                                                              .Where(
                                                                  allergen =>
                                                                  allergensToAvoid.
                                                                      Contains(
                                                                          allergen.Id))
                                                              .Select(
                                                                  allergen =>
                                                                  allergen.Name).OrderBy(a => a)),
                        Name = ingredient.Name,
                        Description = ingredient.Description ?? ""
                    }).ToList();

            res.Add(
                new KeyValuePair<string, IList<IngredientItemModel>>(
                    string.Format("{0} Items you can't have", ingredients.Count), ingredients));

            return res;
        }
    }
}