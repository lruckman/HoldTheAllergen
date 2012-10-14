using System.Collections.Generic;
using System.Globalization;
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
    public class RestaurantIngredientController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantIngredientController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public ActionResult Index(int restaurantId)
        {
            var restaurant = _restaurantRepository.Find(restaurantId);
            var model = MappingEngine.Map<IEnumerable<RestaurantIngredient>, IEnumerable<RestaurantIngredientModel>>(
                    restaurant.Ingredients.OrderBy(ingredient=>ingredient.Name).ToArray());
            ViewBag.Title = restaurant.Name + " Ingredients";
            return View(model);
        }

        [PostRedirectGet.ImportModelState]
        public ActionResult Create(int restaurantId)
        {
            var model =
                MappingEngine.Map<RestaurantIngredient, RestaurantIngredientCreateModel>(
                    new RestaurantIngredient { RestaurantId = restaurantId });
            return PartialView("_create", model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<RestaurantIngredientCreateModel> Create(RestaurantIngredientCreateModel model)
        {
            return Form(model,
               () => Json(new JsonRequestResult<string>(true, "<strong>Well done!</strong> " + model.Name + " created.")),
               () => this.RedirectToAction(action => action.Create(model.RestaurantId)));
        }

        [PostRedirectGet.ImportModelState]
        public ActionResult Delete(int id)
        {
            var restaurantIngredient = _restaurantRepository.GetIngredientById(id);

            if (restaurantIngredient==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var model = MappingEngine.Map<RestaurantIngredient, RestaurantIngredientDeleteModel>(restaurantIngredient);
            return PartialView("_delete", model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<RestaurantIngredientDeleteModel> Delete(RestaurantIngredientDeleteModel model)
        {
            return Form(model,
               () => Json(new JsonRequestResult<string>(true, "<strong>Well done!</strong> Ingredient deleted.")),
               () => this.RedirectToAction(action => action.Delete(model.Id)));
        }

        [PostRedirectGet.ImportModelState]
        public ActionResult Edit(int id)
        {
            var restaurantIngredient = _restaurantRepository.GetIngredientById(id);

            if (restaurantIngredient == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var model =
                MappingEngine.Map<RestaurantIngredient, RestaurantIngredientEditModel>(restaurantIngredient);
            return PartialView("_edit", model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<RestaurantIngredientEditModel> Edit(RestaurantIngredientEditModel model)
        {
            return Form(model,
               () => Json(new JsonRequestResult<string>(true, "<strong>Well done!</strong> " + model.Name + " updated.")),
               () => this.RedirectToAction(action => action.Create(model.Id)));
        }

        public ActionResult Search(string q, int restaurantId)
        {
            return Json(_restaurantRepository.GetIngredientsByRestaurantId(restaurantId)
                            .Where(ingredient => ingredient.Name.ToLowerInvariant().Contains(q.ToLowerInvariant()))
                            .Select(ingredient => new { value = ingredient.Id.ToString(CultureInfo.InvariantCulture), key = ingredient.Name })
                            .OrderBy(ingredient => ingredient.key),
                        JsonRequestBehavior.AllowGet);
        }
    }
}