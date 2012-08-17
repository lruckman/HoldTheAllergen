using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.Data.DataAccess;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.API.Core
{
    public class CustomModelBinderProvider : ModelBinderProvider
    {
        private readonly UserModelBinder _userModelBinder;

        public CustomModelBinderProvider()
        {
            _userModelBinder = new UserModelBinder(IoCHelper.GetInstance<IUserRepository>());
        }

        public override IModelBinder GetBinder(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            return bindingContext.ModelType == typeof (User) ? _userModelBinder : null;
        }
    }
}