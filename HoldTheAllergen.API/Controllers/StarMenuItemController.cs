using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.API.Controllers
{
    public class StarMenuItemController : ApiController
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public StarMenuItemController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        // POST /api/starredrestaurantmenu
        public HttpResponseMessage Post(int id, [ModelBinder(typeof(CustomModelBinderProvider))]User user)
        {
            var menuItem = _restaurantRepository.GetMenuItemById(id);

            if (menuItem == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            if (_restaurantRepository.GetStarredMenuItem(id, user.Id) != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            _restaurantRepository.InsertOnSubmit(new UserStarredMenuItem
                                                     {
                                                         CreateDate = DateTime.UtcNow,
                                                         MenuItemId = menuItem.Id,
                                                         RestaurantId = menuItem.RestaurantId,
                                                         UserId = user.Id
                                                     });
            _restaurantRepository.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE /api/starredrestaurantmenu/5
        public HttpResponseMessage Delete(int id, [ModelBinder(typeof(CustomModelBinderProvider))]User user)
        {
            var starredMenuItem = _restaurantRepository.GetStarredMenuItem(id, user.Id);

            if (starredMenuItem == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _restaurantRepository.DeleteOnSubmit(starredMenuItem);
            _restaurantRepository.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}