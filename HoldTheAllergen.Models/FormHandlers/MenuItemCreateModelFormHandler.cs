using System;
using System.Globalization;
using System.Linq;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class MenuItemCreateModelFormHandler : IFormHandler<MenuItemCreateModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public MenuItemCreateModelFormHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Handle(MenuItemCreateModel form)
        {
            var restaurant = _restaurantRepository.Find(form.RestaurantId);

            var menuItem = new MenuItem
                               {
                                   DateAdded = DateTime.UtcNow,
                                   Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(form.Name.ToLowerInvariant()).Trim(),
                                   GroupName = form.GroupName.Trim()
                               };

            foreach (var ingredient in form.IngredientIds.Select(id => _restaurantRepository.GetIngredientById(id)))
            {
                menuItem.Ingredients.Add(ingredient);
            }

            restaurant.MenuItems.Add(menuItem);

            _restaurantRepository.SaveChanges();
        }
    }
}