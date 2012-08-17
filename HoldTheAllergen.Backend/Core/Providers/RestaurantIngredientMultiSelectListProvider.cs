using System.Collections;
using System.Linq;
using System.Web.Mvc;
using HoldTheAllergen.Data.DataAccess;

namespace HoldTheAllergen.Backend.Core.Providers
{
    public class RestaurantIngredientMultiSelectListProvider
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantIngredientMultiSelectListProvider(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public MultiSelectList Provide(int restaurantId, IEnumerable selected)
        {
            var ingredients = _restaurantRepository.GetById(restaurantId).Ingredients.ToArray();
            var menuItem = ingredients
                .Select(ingredient => new {ingredient.Id, Name = ingredient.Name})
                .OrderBy(ingredient => ingredient.Name);
            return new MultiSelectList(menuItem, "Id", "Name", selected);
        }
    }
}