using System;
using System.Collections.Generic;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        RestaurantIngredient GetIngredientById(int id);
        IEnumerable<RestaurantCategory> GetCategories();
        MenuItem GetMenuItemById(int id);
        MenuItem GetFullMenuItemDetailsById(int id);
        IEnumerable<RestaurantIngredient> GetIngredientsByRestaurantId(int restaurantId);
        IRepository<Restaurant> InsertOnSubmit(RestaurantIngredient entity);
        IRepository<Restaurant> DeleteOnSubmit(MenuItem entity);
        RestaurantLocation GetLocationById(int id);
        UserStarredMenuItem GetStarredMenuItem(int menuItemId, Guid userId);
        IRepository<Restaurant> InsertOnSubmit(UserStarredMenuItem entity);
        IRepository<Restaurant> DeleteOnSubmit(UserStarredMenuItem entity);
        IRepository<Restaurant> DeleteOnSubmit(RestaurantIngredient entity);
        IEnumerable<UserStarredMenuItem> GetStarredMenuItems(int restaurantId, Guid userId);
        IEnumerable<MenuItem> GetMenuItems(int restaurantId);
    }
}