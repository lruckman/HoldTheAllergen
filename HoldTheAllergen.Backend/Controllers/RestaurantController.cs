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
    public class RestaurantController : DefaultController
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [System.Web.Mvc.HttpGet]
        [PostRedirectGet.ImportModelState]
        public ActionResult Create()
        {
            var model = MappingEngine.Map<Restaurant, RestaurantCreateModel>(new Restaurant());
            return PartialView("_create",model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<RestaurantCreateModel> Create(RestaurantCreateModel model)
        {
            return Form(model, () => Json(new JsonRequestResult<string>(true,"")),
                        () => this.RedirectToAction(action => action.Create()));
        }

        [PostRedirectGet.ImportModelState]
        public ActionResult Delete(int id)
        {
            var restaurant = _restaurantRepository.Find(id);

            if (restaurant == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var model = MappingEngine.Map<Restaurant, RestaurantDeleteModel>(restaurant);
            return PartialView("_delete", model);
        }

        [System.Web.Mvc.HttpPost]
        [PostRedirectGet.ExportModelState]
        public FormActionResult<RestaurantDeleteModel> Delete(RestaurantDeleteModel model)
        {
            return Form(model,
               () => Json(new JsonRequestResult<string>(true,"")),
               () => this.RedirectToAction(action => action.Delete(model.Id)));
        }

        public ActionResult Details(int id)
        {
            var restaurant = _restaurantRepository.Find(id);

            if (restaurant == null)
            {
                return InvokeHttpError(HttpContext, HttpStatusCode.NotFound);
            }

            var model = MappingEngine.Map<Restaurant, RestaurantModel>(restaurant);
            return View(model);
        }
        
        public ActionResult Index()
        {
            var restaurants = _restaurantRepository.All().OrderBy(restaurant => restaurant.Name);
            var model = MappingEngine.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantModel>>(restaurants.ToArray());

            return View(model);
        }
    }
}