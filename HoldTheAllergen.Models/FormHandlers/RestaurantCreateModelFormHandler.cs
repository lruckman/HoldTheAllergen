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
            var restaurant = _restaurantRepository.CreateNew();
            restaurant.DateAdded = DateTime.UtcNow;
            restaurant.Name = form.Name.Trim();
            restaurant.CategoryName = "Fast Food";
            restaurant.Logo = string.Format("~/images/logos/{0}.jpg", form.Name.ToLowerInvariant().Replace(" ",""));
            restaurant.UrlSlug = "";

            // save logo

            _restaurantRepository.InsertOnSubmit(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}