using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.API.Controllers
{
    public class StarMenuItemController : ApiController
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;

        public StarMenuItemController(IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        // POST /api/starredrestaurantmenu
        public HttpResponseMessage Post(int id, Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            var menuItem = _restaurantRepository.GetMenuItemById(id);

            if (menuItem == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (_restaurantRepository.GetStarredMenuItem(id, user.Id) != null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }

            _restaurantRepository.Create(new UserStarredMenuItem
                {
                    CreateDate = DateTime.UtcNow,
                    MenuItemId = menuItem.Id,
                    RestaurantId = menuItem.RestaurantId,
                    UserId = user.Id
                });

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE /api/starredrestaurantmenu/5
        public HttpResponseMessage Delete(int id, Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            var starredMenuItem = _restaurantRepository.GetStarredMenuItem(id, userId);

            if (starredMenuItem == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _restaurantRepository.Delete(starredMenuItem);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}