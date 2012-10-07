using System;
using System.Globalization;
using System.Linq;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class RestaurantIngredientCreateModelHandler : IFormHandler<RestaurantIngredientCreateModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IAllergenRepository _allergenRepository;

        public RestaurantIngredientCreateModelHandler(IRestaurantRepository restaurantRepository,
                                                      IAllergenRepository allergenRepository)
        {
            _restaurantRepository = restaurantRepository;
            _allergenRepository = allergenRepository;
        }

        public void Handle(RestaurantIngredientCreateModel form)
        {
            var ingredientDescription = form.Description
                    .Replace(Environment.NewLine, " ")
                    .Replace("  ", " ")
                    .Replace("  ", " ")
                    .Trim();

            var restaurantIngredient = new RestaurantIngredient
                                           {
                                               DateAdded = DateTime.UtcNow,
                                               Description = ingredientDescription,
                                               Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(form.Name.ToLowerInvariant()).Trim(),
                                               RestaurantId = form.RestaurantId
                                           };

            if (form.AllergenIds != null)
            {
                foreach (var allergen in _allergenRepository.All()
                    .Where(allergen => form.AllergenIds.Contains(allergen.Id)).ToArray())
                {
                    restaurantIngredient.Allergens.Add(allergen);
                }
            }
            
            if (form.CreateMenuItem)
            {
                restaurantIngredient.MenuItems.Add(new MenuItem
                {
                    DateAdded = DateTime.UtcNow,
                    GroupName = form.GroupName,
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(form.Name.ToLowerInvariant()).Trim(),
                    RestaurantId = form.RestaurantId
                });
            }
            
            _restaurantRepository.Create(restaurantIngredient);
        }
    }
}