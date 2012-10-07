using System;
using System.Collections.Generic;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class MenuItemIngredientsController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;

        public MenuItemIngredientsController(IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        // GET /api/menuitemingredients/5
        public IList<KeyValuePair<string, IList<IngredientItemModel>>> Get(int id, Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            var menuItem = _restaurantRepository.GetFullMenuItemDetailsById(id);
            return MenuItemIngredientsModelMapper.Map(menuItem, user);
        }
    }
}