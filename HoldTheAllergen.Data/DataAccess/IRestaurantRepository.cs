using System;
using System.Collections.Generic;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        RestaurantIngredient GetIngredientById(int id);
        //IEnumerable<RestaurantCategory> GetCategories();
        MenuItem GetMenuItemById(int id);
        MenuItem GetFullMenuItemDetailsById(int id);
        IEnumerable<RestaurantIngredient> GetIngredientsByRestaurantId(int restaurantId);
        int Create(RestaurantIngredient entity);
        int Delete(MenuItem entity);
        //RestaurantLocation GetLocationById(int id);
        UserStarredMenuItem GetStarredMenuItem(int menuItemId, Guid userId);
        int Create(UserStarredMenuItem entity);
        int Delete(UserStarredMenuItem entity);
        int Delete(RestaurantIngredient entity);
        IEnumerable<UserStarredMenuItem> GetStarredMenuItems(int restaurantId, Guid userId);
        IEnumerable<MenuItem> GetMenuItems(int restaurantId);
    }
}