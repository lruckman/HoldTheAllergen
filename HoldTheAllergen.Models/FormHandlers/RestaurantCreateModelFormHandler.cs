using System;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class RestaurantCreateModelHandler : IFormHandler<RestaurantCreateModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantCreateModelHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Handle(RestaurantCreateModel form)
        {
            var restaurant = new Restaurant
                {
                    DateAdded = DateTime.UtcNow,
                    Name = form.Name.Trim(),
                    CategoryName = "Fast Food",
                    Logo = string.Format("~/images/logos/{0}.jpg", form.Name.ToLowerInvariant().Replace(" ", "")),
                    UrlSlug = ""
                };

            // save logo

            _restaurantRepository.Create(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}