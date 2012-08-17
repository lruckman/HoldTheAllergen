using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class RestaurantsController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        // GET /api/restaurants
        public HttpResponseMessage<IEnumerable<RestaurantListItemModel>> Get(
            [ModelBinder(typeof (CustomModelBinderProvider))] User user)
        {
            var restaurants = _restaurantRepository.All().Where(x=>x.MenuItems.Any()).OrderBy(restaurant => restaurant.Name);
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
                                                     Id = r.Id,
                                                     Logo = r.Logo.Replace("~", "http://api.holdtheallergen.com"),
                                                     Name = r.Name
                                                 });

            return new HttpResponseMessage<IEnumerable<RestaurantListItemModel>>(model, HttpStatusCode.OK);
        }
    }
}