using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HoldTheAllergen.Data.DataAccess;

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
        public HttpResponseMessage Post(int id, Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (user.Allergies.Any(allergy => allergy.Id == id))
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }

            var allergen = _allergenRepository.Find(id);

            if (allergen == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            user.Allergies.Add(allergen);
            _userRepository.SaveChanges();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE /api/userallergy/5
        public HttpResponseMessage Delete(int id, Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (user.Allergies.All(allergy => allergy.Id != id))
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var allergen = _allergenRepository.Find(id);

            if (allergen == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            user.Allergies.Remove(allergen);
            _userRepository.SaveChanges();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}