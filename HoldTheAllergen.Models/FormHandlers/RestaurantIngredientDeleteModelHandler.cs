using System.Linq;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class RestaurantIngredientDeleteModelHandler : IFormHandler<RestaurantIngredientDeleteModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantIngredientDeleteModelHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Handle(RestaurantIngredientDeleteModel form)
        {
            var restaurantIngredient = _restaurantRepository.GetIngredientById(form.Id);

            if (restaurantIngredient == null)
            {
                return;
            }

            var menuItems = _restaurantRepository.GetMenuItems(restaurantIngredient.RestaurantId);
            var orphans = menuItems
                .Where(menuItem =>menuItem.Ingredients.Count() == 1 
                    && menuItem.Ingredients.First().Id == restaurantIngredient.Id);

            foreach (var orphan in orphans)
            {
                _restaurantRepository.Delete(orphan);
            }

            _restaurantRepository.Delete(restaurantIngredient);
        }
    }
}