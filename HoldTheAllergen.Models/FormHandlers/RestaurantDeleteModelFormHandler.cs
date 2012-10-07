using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class RestaurantDeleteModelFormHandler : IFormHandler<RestaurantDeleteModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantDeleteModelFormHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Handle(RestaurantDeleteModel form)
        {
            var restaurant = _restaurantRepository.Find(form.Id);

            if (restaurant == null)
            {
                return;
            }

            _restaurantRepository.Delete(restaurant);
        }
    }
}