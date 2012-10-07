using System;
using System.Collections.Generic;
using System.Linq;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class RestaurantsController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;

        public RestaurantsController(IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        // GET /api/restaurants
        public IList<RestaurantListItemModel> Get(Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            var restaurants =
                _restaurantRepository.All().Where(x => x.MenuItems.Any()).OrderBy(restaurant => restaurant.Name);
            var model =
                restaurants.ToArray().Select(r =>
                                             new RestaurantListItemModel
                                                 {
                                                     MenuAction =
                                                         string.Format("{0}/{1}/restaurant/{2}/menu", Constants.BaseUrl,
                                                                       user.Id, r.Id),
                                                     StarredMenuAction =
                                                         string.Format("{0}/{1}/restaurant/{2}/menu/starred",
                                                                       Constants.BaseUrl, user.Id, r.Id),
                                                     AllergyFreeMenuAction =
                                                         string.Format("{0}/{1}/restaurant/{2}/menu/allergyfree",
                                                                       Constants.BaseUrl, user.Id, r.Id),
                                                     Id = r.Id,
                                                     Logo = r.Logo.Replace("~", "http://api.holdtheallergen.com"),
                                                     Name = r.Name
                                                 });

            return model.ToList();
        }
    }
}