using System;
using System.Collections.Generic;
using System.Linq;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Core
{
    public class RestaurantMenuModelMapper
    {
        public static IEnumerable<RestaurantMenuModel> Map(IEnumerable<MenuItem> menuItems, User user, bool onlyStarred)
        {
            return MapMenuGroups(menuItems, user, onlyStarred);
        }

        private static IEnumerable<RestaurantMenuModel> MapMenuGroups(IEnumerable<MenuItem> menuItems,
                                                                      User user,
                                                                      bool onlyStarred)
        {
            var starredItems = user.StarredMenuItems.Select(x => x.MenuItemId).ToArray();
            var groups = (onlyStarred
                              ? menuItems.Where(menuItem => (starredItems.Any(f => f == menuItem.Id)))
                              : menuItems)
                .OrderBy(menuItem => menuItem.GroupName)
                .GroupBy(menuItem => menuItem.GroupName);
            var allergensToAvoid = user.Allergies.ToArray();

            return
                groups.Select(
                    menuGroup =>
                    new RestaurantMenuModel
                        {
                            CategoryName = menuGroup.Key,
                            Items =
                                MapMenuItems(menuGroup, allergensToAvoid, starredItems, user.Id, onlyStarred).OrderBy(
                                    item => item.Name)
                        });
        }

        private static IEnumerable<MenuItemModel> MapMenuItems(IEnumerable<MenuItem> entities,
                                                               Allergen[] allergensToAvoid,
                                                               int[] starredMenuItems,
                                                                Guid uid,
                                                               bool onlyStarred)
        {
            var menuItems = onlyStarred
                                ? entities.Where(e => (starredMenuItems.Any(f => f == e.Id))).ToArray()
                                : entities.ToArray();

            foreach (var item in menuItems)
            {
                var allergenIds = item.Ingredients.SelectMany(i => i.Allergens).Select(i => i.Id).Distinct().ToArray();
                yield return new MenuItemModel
                                 {
                                     Action = string.Format("{0}/{1}/menuitem/{2}/ingredients", Constants.BaseUrl, uid, item.Id),
                                     AllergenNames =
                                         string.Join(", ",
                                                     allergensToAvoid.Where(a => allergenIds.Contains(a.Id)).Select(
                                                         a => a.Name).OrderBy(a => a)),
                                     StarAction = string.Format("{0}/{1}/menuitem/{2}/star", Constants.BaseUrl, uid, item.Id),
                                     Starred = (starredMenuItems.Any(f => f == item.Id)),
                                     Id = item.Id,
                                     Name = item.Name
                                 };
            }
        }
    }
}