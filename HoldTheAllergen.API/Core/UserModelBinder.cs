using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;
using StructureMap;

namespace HoldTheAllergen.API.Core
{
    public class UserModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var userRepository = ObjectFactory.GetInstance<IUserRepository>();
            Guid userId;
            Guid.TryParse(bindingContext.ValueProvider.GetValue("userId").AttemptedValue, out userId);

            var user = userRepository.GetUser(userId);

            if (user == null)
            {
                user = new User {CreateDate = DateTime.Now, LastActivity = DateTime.UtcNow, Id = userId};
                userRepository.Create(user);
            }
            else
            {
                user.LastActivity = DateTime.UtcNow;
                userRepository.Update(user);
            }

            bindingContext.Model = user;

            return true;
        }
    }
}