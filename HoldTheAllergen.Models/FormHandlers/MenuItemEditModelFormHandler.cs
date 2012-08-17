using System;
using System.Globalization;
using System.Linq;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class MenuItemEditModelFormHandler : IFormHandler<MenuItemEditModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public MenuItemEditModelFormHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Handle(MenuItemEditModel form)
        {
            var menuItem = _restaurantRepository.GetMenuItemById(form.Id);

            if (menuItem == null)
            {
                return;
            }

            menuItem.DateUpdated = DateTime.UtcNow;
            menuItem.GroupName = form.GroupName;
            menuItem.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(form.Name.ToLowerInvariant()).Trim();

            // remove the removed
            foreach (var ingredient in menuItem.Ingredients.ToArray()
                .Where(ingredient => !form.IngredientIds.Contains(ingredient.Id)))
            {
                menuItem.Ingredients.Remove(ingredient);
            }
            // add the new
            var currentIngredientIds = menuItem.Ingredients.Select(x => x.Id).ToArray();
            foreach (var ingredient in from ingredientId in form.IngredientIds
                                       where !currentIngredientIds.Contains(ingredientId)
                                       select _restaurantRepository.GetIngredientById(ingredientId)
                                       into ingredient where ingredient != null select ingredient)
            {
                menuItem.Ingredients.Add(ingredient);
            }

            _restaurantRepository.SaveChanges();
        }
    }
}