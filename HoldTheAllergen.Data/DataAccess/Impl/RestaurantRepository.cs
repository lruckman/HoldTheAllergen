using System;
using System.Collections.Generic;
using System.Linq;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        private readonly HoldTheAllergenEntities _context;

        public RestaurantRepository(HoldTheAllergenEntities context)
            : base(context)
        {
            _context = context;
        }

        #region IRestaurantRepository Members

        public override Restaurant GetById(int id)
        {
            return _context.Restaurants
                .Include("Ingredients")
                .Include("Ingredients.Allergens")
                .Include("MenuItems")
                .FirstOrDefault(restaurant => restaurant.Id == id);
        }

        public RestaurantIngredient GetIngredientById(int id)
        {
            return _context.RestaurantIngredients.FirstOrDefault(ingredient => ingredient.Id == id);
        }

        public IEnumerable<RestaurantCategory> GetCategories()
        {
            return _context.RestaurantCategories.OrderBy(category => category.Name).ToArray();
        }

        public MenuItem GetMenuItemById(int id)
        {
            return _context.MenuItems.FirstOrDefault(menuItem => menuItem.Id == id);
        }

        public MenuItem GetFullMenuItemDetailsById(int id)
        {
            return
                _context.MenuItems.Include("Restaurant").Include("Ingredients").Include("Ingredients.Allergens").
                    FirstOrDefault(menuItem => menuItem.Id == id);
        }

        public IEnumerable<RestaurantIngredient> GetIngredientsByRestaurantId(int restaurantId)
        {
            return _context.RestaurantIngredients
                .Where(ingredient => ingredient.RestaurantId == restaurantId)
                .OrderBy(ingredient => ingredient.Name)
                .ToArray();
        }

        public IRepository<Restaurant> InsertOnSubmit(RestaurantIngredient entity)
        {
            _context.RestaurantIngredients.AddObject(entity);
            return this;
        }

        public IRepository<Restaurant> DeleteOnSubmit(MenuItem entity)
        {
            _context.MenuItems.DeleteObject(entity);
            return this;
        }

        public RestaurantLocation GetLocationById(int id)
        {
            return _context.RestaurantLocations.Include("Restaurant").FirstOrDefault(location => location.Id == id);
        }

        public UserStarredMenuItem GetStarredMenuItem(int menuItemId, Guid userId)
        {
            return _context.UserStarredMenuItems.FirstOrDefault(fav => fav.UserId == userId && fav.MenuItemId == menuItemId);
        }

        public IRepository<Restaurant> InsertOnSubmit(UserStarredMenuItem entity)
        {
            _context.UserStarredMenuItems.AddObject(entity);
            return this;
        }

        public IRepository<Restaurant> DeleteOnSubmit(UserStarredMenuItem entity)
        {
            _context.UserStarredMenuItems.DeleteObject(entity);
            return this;
        }

        public IRepository<Restaurant> DeleteOnSubmit(RestaurantIngredient entity)
        {
            _context.RestaurantIngredients.DeleteObject(entity);
            return this;
        }

        public IEnumerable<MenuItem> GetMenuItems(int restaurantId)
        {
            return
                _context.MenuItems.Include( "Ingredients").Where(menuItem => menuItem.RestaurantId == restaurantId).OrderBy(
                    menuItem => menuItem.GroupName).ThenBy(menuItem => menuItem.Name).ToArray();
        }


        public IEnumerable<UserStarredMenuItem> GetStarredMenuItems(int restaurantId, Guid userId)
        {
            return _context.UserStarredMenuItems.Where(fav => fav.UserId == userId && fav.RestaurantId == restaurantId);
        }

        #endregion
    }
}