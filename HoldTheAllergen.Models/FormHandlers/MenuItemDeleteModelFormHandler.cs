using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Models.FormHandlers
{
    public class MenuItemDeleteModelFormHandler : IFormHandler<MenuItemDeleteModel>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public MenuItemDeleteModelFormHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Handle(MenuItemDeleteModel form)
        {
            var menuItem = _restaurantRepository.GetMenuItemById(form.Id);

            if (menuItem == null)
            {
                return;
            }
            _restaurantRepository.DeleteOnSubmit(menuItem).SaveChanges();
        }
    }
}