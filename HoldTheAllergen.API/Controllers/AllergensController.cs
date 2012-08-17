using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class AllergensController : ApiController
    {
        private readonly IAllergenRepository _allergenRepository;

        public AllergensController(IAllergenRepository allergenRepository)
        {
            _allergenRepository = allergenRepository;
        }

        // GET /api/allergens/5
        public IEnumerable<AllergenItemModel> Get([ModelBinder(typeof (CustomModelBinderProvider))] User user)
        {
            var allergens = _allergenRepository.GetAllergens();
            var allergies = user.Allergies.ToArray();
            var model = allergens.Select(allergen =>
                                         new AllergenItemModel
                                             {
                                                 Id = allergen.Id,
                                                 Name = allergen.Name,
                                                 Selected = allergies.Any(allergy => allergy.Id == allergen.Id),
                                                 SelectAction =
                                                     string.Format("{0}/{1}/userallergy/{2}", Constants.BaseUrl,
                                                                   user.Id, allergen.Id)
                                             }).ToArray();
            return model;
        }
    }
}