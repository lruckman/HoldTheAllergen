using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using HoldTheAllergen.Backend.Core;
using HoldTheAllergen.Backend.Core.Helpers;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models;

namespace HoldTheAllergen.Backend.Controllers
{
    public class MenuItemController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public MenuItemController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [System.Web.Mvc.HttpGet]
        [PostRedirectGet.ImportModelState]
        public ActionResult Create(int restaurantId)
        {
            var model = MappingEngine.Map<MenuItem, MenuItemCreateModel>(new MenuItem { RestaurantId = restaurantId });
            return PartialView("_create",model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<MenuItemCreateModel> Create(MenuItemCreateModel model)
        {            return Form(model,
                        () => Json(new JsonRequestResult<string>(true, "<strong>Well done!</strong> " + model.Name + " created.")),
                        () => this.RedirectToAction(action => action.Create(model.RestaurantId)));
        }

        [PostRedirectGet.ImportModelState]
        public ActionResult Delete(int id)
        {
            var menuItem = _restaurantRepository.GetMenuItemById(id);

            if (menuItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var model = MappingEngine.Map<MenuItem, MenuItemDeleteModel>(menuItem);
            return PartialView("_delete", model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<MenuItemDeleteModel> Delete(MenuItemDeleteModel model)
        {
            return Form(model,
               () => Json(new JsonRequestResult<string>(true, "<strong>Well done!</strong> Menu Item deleted.")),
               () => this.RedirectToAction(action => action.Delete(model.Id)));
        }

        [System.Web.Mvc.HttpGet]
        [PostRedirectGet.ImportModelState]
        public ActionResult Edit(int id)
        {
            var menuItem = _restaurantRepository.GetMenuItemById(id);

            if (menuItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var model = MappingEngine.Map<MenuItem, MenuItemEditModel>(menuItem);
            return PartialView("_edit", model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<MenuItemEditModel> Edit(MenuItemEditModel model)
        {
            return Form(model,
               () => Json(new JsonRequestResult<string>(true, "<strong>Well done!</strong> " + model.Name + " updated.")),
               () => this.RedirectToAction(action => action.Edit(model.Id)));
        }

        public ActionResult Index(int restaurantId)
        {
            var restaurant = _restaurantRepository.GetById(restaurantId);
            var model =
                MappingEngine.Map<IEnumerable<MenuItem>, IEnumerable<MenuItemModel>>(
                    restaurant.MenuItems.OrderBy(menuItem => menuItem.Name));
            ViewBag.Title = restaurant.Name + " Menu Items";
            return View("index", model);
        }
    }
}