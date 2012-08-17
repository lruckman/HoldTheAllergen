using System.Collections.Generic;
using System.Linq;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Core
{
    public class MenuItemIngredientsModelMapper
    {
        public static Dictionary<string, IEnumerable<IngredientItemModel>> Map(MenuItem menuItem, User user)
        {
            var allergensToAvoid = user.Allergies.Select(allergen => allergen.Id).ToArray();
            var res = new Dictionary<string, IEnumerable<IngredientItemModel>>();

            var ingredients = menuItem.Ingredients
                .Where(ingredient => !ingredient.Allergens.Any(allergen => allergensToAvoid.Contains(allergen.Id)))
                .OrderBy(ingredient => ingredient.Name)
                .Select(ingredient => new IngredientItemModel
                                          {
                                              AllergenNames = string.Empty,
                                              Name = ingredient.Name,
                                              Description = ingredient.Description ?? ""
                                          })
                .ToArray();

            res.Add(string.Format("{0} Items you can have", ingredients.Length), ingredients);

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
                                                                                    allergen.Name).OrderBy(a=>a)),
                                              Name = ingredient.Name,
                                              Description = ingredient.Description ?? ""
                                          }).ToArray();

            res.Add(string.Format("{0} Items you can't have", ingredients.Length), ingredients);

            return res;
        }
    }
}