using System;
using System.Globalization;
using System.Linq;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class RestaurantIngredientEditModelHandler : IFormHandler<RestaurantIngredientEditModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IAllergenRepository _allergenRepository;

        public RestaurantIngredientEditModelHandler(IRestaurantRepository restaurantRepository,
                                                      IAllergenRepository allergenRepository)
        {
            _restaurantRepository = restaurantRepository;
            _allergenRepository = allergenRepository;
        }

        public void Handle(RestaurantIngredientEditModel form)
        {
            var restaurantIngredient = _restaurantRepository.GetIngredientById(form.Id);

            var ingredientDescription = form.Description
                    .Replace(Environment.NewLine, " ")
                    .Replace("  ", " ")
                    .Replace("  ", " ")
                    .Trim();

            restaurantIngredient.DateUpdated = DateTime.UtcNow;
            restaurantIngredient.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(form.Name.ToLowerInvariant()).Trim();
            restaurantIngredient.Description = ingredientDescription;

            restaurantIngredient.Allergens.Clear();

            if (form.AllergenIds != null)
            {
                foreach (var allergen in _allergenRepository.All()
                    .Where(allergen => form.AllergenIds.Contains(allergen.Id)).ToArray())
                {
                    restaurantIngredient.Allergens.Add(allergen);
                }
            }

            _restaurantRepository.SaveChanges();
        }
    }
}