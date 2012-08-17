using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.API.Core
{
    public class UserModelBinder : IModelBinder
    {
        private readonly IUserRepository _userRepository;

        public UserModelBinder(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            Guid userId;
            Guid.TryParse(bindingContext.ValueProvider.GetValue("user").AttemptedValue, out userId);

            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                user = new User {CreateDate = DateTime.Now, LastActivity = DateTime.UtcNow, Id = userId};
                _userRepository.InsertOnSubmit(user).SaveChanges();
            }
            else
            {
                user.LastActivity = DateTime.UtcNow;
                _userRepository.SaveChanges();
            }

            bindingContext.Model = user;

            return true;
        }
    }
}