using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.API.Core;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.API.Controllers
{
    public class UserAllergyController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IAllergenRepository _allergenRepository;

        public UserAllergyController(IUserRepository userRepository, IAllergenRepository allergenRepository)
        {
            _userRepository = userRepository;
            _allergenRepository = allergenRepository;
        }

        // POST /api/userallergy
        public HttpResponseMessage Post(int id, [ModelBinder(typeof(CustomModelBinderProvider))]User user)
        {
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (user.Allergies.Any(allergy => allergy.Id == id))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            var allergen = _allergenRepository.GetById(id);

            if (allergen == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            user.Allergies.Add(allergen);
            _userRepository.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE /api/userallergy/5
        public HttpResponseMessage Delete(int id, [ModelBinder(typeof(CustomModelBinderProvider))]User user)
        {
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (user.Allergies.All(allergy => allergy.Id != id))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var allergen = _allergenRepository.GetById(id);

            if (allergen == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            user.Allergies.Remove(allergen);
            _userRepository.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}