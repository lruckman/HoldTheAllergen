using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Models.API;

namespace HoldTheAllergen.API.Controllers
{
    public class AllergensController : ApiController
    {
        private readonly IAllergenRepository _allergenRepository;
        private readonly IUserRepository _userRepository;

        public AllergensController(IAllergenRepository allergenRepository, IUserRepository userRepository)
        {
            _allergenRepository = allergenRepository;
            _userRepository = userRepository;
        }

        // GET /api/allergens/5
        public IList<AllergenItemModel> Get(Guid userId)
        {
            var user = _userRepository.GetUser(userId);
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