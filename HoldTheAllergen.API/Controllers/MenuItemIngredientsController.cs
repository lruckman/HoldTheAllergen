using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class MenuItemIngredientsController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public MenuItemIngredientsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        // GET /api/menuitemingredients/5
        public Dictionary<string, IEnumerable<IngredientItemModel>> Get(int id,
                                                                        [ModelBinder(typeof (CustomModelBinderProvider))] User user)
        {
            var menuItem = _restaurantRepository.GetFullMenuItemDetailsById(id);
            return MenuItemIngredientsModelMapper.Map(menuItem, user);
        }
    }
}