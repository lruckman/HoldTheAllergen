using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class RestaurantMenuController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantMenuController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public HttpResponseMessage<IEnumerable<RestaurantMenuModel>> Get(int restaurantId,
                                                                         [ModelBinder(typeof (CustomModelBinderProvider)
                                                                             )] User user)
        {
            var menuItems = _restaurantRepository.GetMenuItems(restaurantId);

            var model = RestaurantMenuModelMapper.Map(menuItems, user, false);

            return new HttpResponseMessage<IEnumerable<RestaurantMenuModel>>(model, HttpStatusCode.OK);
        }
    }
}