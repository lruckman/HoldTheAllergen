using System;
using System.Collections.Generic;
using System.Linq;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Core
{
    public class RestaurantMenuModelMapper
    {
        public static IList<RestaurantMenuModel> Map(IEnumerable<MenuItem> menuItems, User user, bool onlyStarred, bool allergyFree)
        {
            return MapMenuGroups(menuItems, user, onlyStarred, allergyFree);
        }

        private static IList<RestaurantMenuModel> MapMenuGroups(IEnumerable<MenuItem> menuItems,
                                                                      User user, bool onlyStarred, bool allergyFree)
        {
            var starredItems = user.StarredMenuItems.Select(x => x.MenuItemId).ToArray();

            var groups = (onlyStarred
                              ? menuItems.Where(menuItem => (starredItems.Any(f => f == menuItem.Id)))
                              : menuItems)
                .OrderBy(menuItem => menuItem.GroupName)
                .GroupBy(menuItem => menuItem.GroupName)
                .ToList();

            var allergensToAvoid = user.Allergies.ToArray();

            return
                groups.Select(
                    menuGroup =>
                    new RestaurantMenuModel
                        {
                            CategoryName = menuGroup.Key,
                            Items =
                                MapMenuItems(menuGroup, allergensToAvoid, starredItems, user.Id, onlyStarred, allergyFree)
                                .OrderBy(item => item.Name)
                                .ToArray()
                        }).ToList();
        }

        private static IEnumerable<MenuItemModel> MapMenuItems(IEnumerable<MenuItem> entities,
                                                               Allergen[] allergensToAvoid, int[] starredMenuItems,
                                                                Guid uid, bool onlyStarred, bool allergyFree)
        {
            var menuItems = onlyStarred
                                ? entities.Where(e => (starredMenuItems.Any(f => f == e.Id))).ToArray()
                                : entities.ToArray();

            var results = new List<MenuItemModel>();

            foreach (var item in menuItems)
            {

                var allergenIds = item.Ingredients
                    .SelectMany(i => i.Allergens)
                    .Select(i => i.Id)
                    .Distinct()
                    .ToArray();
                
                var allergenNames = string.Join(", ", allergensToAvoid
                    .Where(a => allergenIds.Contains(a.Id))
                    .Select(a => a.Name)
                    .OrderBy(a => a));

                if (allergyFree && !string.IsNullOrWhiteSpace(allergenNames))
                    {
                        continue;
                    }
                
                results.Add(new MenuItemModel
                                 {
                                     Action = string.Format("{0}/{1}/menuitem/{2}/ingredients", Constants.BaseUrl, uid, item.Id),
                                     AllergenNames = allergenNames,
                                     StarAction = string.Format("{0}/{1}/menuitem/{2}/star", Constants.BaseUrl, uid, item.Id),
                                     Starred = (starredMenuItems.Any(f => f == item.Id)),
                                     Id = item.Id,
                                     Name = item.Name
                                 });
            }
            return results.ToArray();
        }
    }
}