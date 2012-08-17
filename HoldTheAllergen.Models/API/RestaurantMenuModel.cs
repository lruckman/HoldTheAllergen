using System.Collections.Generic;

namespace HoldTheAllergen.Models.API
{
    public class RestaurantMenuModel
    {
        public string CategoryName { get; set; }
        public IEnumerable<MenuItemModel> Items { get; set; }
    }
}