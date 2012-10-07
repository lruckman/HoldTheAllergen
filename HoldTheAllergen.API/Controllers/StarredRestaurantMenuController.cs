using System;
using System.Collections.Generic;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class StarredRestaurantMenuController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;

        public StarredRestaurantMenuController(IRestaurantRepository restaurantRepository,
                                               IUserRepository userRepository)
        {
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        // GET /api/starredrestaurantmenu/5
        public IList<RestaurantMenuModel> Get(int restaurantId, Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            var menuItems = _restaurantRepository.GetMenuItems(restaurantId);

            var model = RestaurantMenuModelMapper.Map(menuItems, user, true, false);

            return model;
        }
    }
}