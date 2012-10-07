using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DbContext context)
            : base(context)
        {
        }

        #region IRestaurantRepository Members

        public Restaurant GetById(int id)
        {
            return Context.Set<Restaurant>()
                .Include(x => x.Ingredients)
                .Include("Ingredients.Allergens")
                .Include(x => x.MenuItems)
                .FirstOrDefault(restaurant => restaurant.Id == id);
        }

        public RestaurantIngredient GetIngredientById(int id)
        {
            return Context.Set<RestaurantIngredient>()
                .FirstOrDefault(ingredient => ingredient.Id == id);
        }

        public MenuItem GetMenuItemById(int id)
        {
            return Context.Set<MenuItem>()
                .FirstOrDefault(menuItem => menuItem.Id == id);
        }

        public MenuItem GetFullMenuItemDetailsById(int id)
        {
            return
                Context.Set<MenuItem>()
                    .Include(x => x.Restaurant)
                    .Include(x => x.Ingredients)
                    .Include("Ingredients.Allergens")
                    .FirstOrDefault(menuItem => menuItem.Id == id);
        }

        public IEnumerable<RestaurantIngredient> GetIngredientsByRestaurantId(int restaurantId)
        {
            return Context.Set<RestaurantIngredient>()
                .Where(ingredient => ingredient.RestaurantId == restaurantId)
                .OrderBy(ingredient => ingredient.Name)
                .ToArray();
        }

        public int Create(RestaurantIngredient entity)
        {
            Context.Set<RestaurantIngredient>().Add(entity);
            return Context.SaveChanges();
        }

        public int Delete(MenuItem entity)
        {
            Context.Set<MenuItem>().Remove(entity);
            return Context.SaveChanges();
        }

        public UserStarredMenuItem GetStarredMenuItem(int menuItemId, Guid userId)
        {
            return Context.Set<UserStarredMenuItem>()
                .FirstOrDefault(fav => fav.UserId == userId && fav.MenuItemId == menuItemId);
        }

        public int Create(UserStarredMenuItem entity)
        {
            Context.Set<UserStarredMenuItem>().Add(entity);
            return Context.SaveChanges();
        }

        public int Delete(UserStarredMenuItem entity)
        {
            Context.Set<UserStarredMenuItem>().Remove(entity);
            return Context.SaveChanges();
        }

        public int Delete(RestaurantIngredient entity)
        {
            Context.Set<RestaurantIngredient>().Remove(entity);
            return Context.SaveChanges();
        }

        public IEnumerable<MenuItem> GetMenuItems(int restaurantId)
        {
            return
                Context.Set<MenuItem>()
                    .Include(x => x.Ingredients)
                    .Where(menuItem => menuItem.RestaurantId == restaurantId)
                    .OrderBy(menuItem => menuItem.GroupName)
                    .ThenBy(menuItem => menuItem.Name)
                    .ToArray();
        }


        public IEnumerable<UserStarredMenuItem> GetStarredMenuItems(int restaurantId, Guid userId)
        {
            return Context.Set<UserStarredMenuItem>()
                .Where(fav => fav.UserId == userId && fav.RestaurantId == restaurantId)
                .ToArray();
        }

        #endregion
    }
}