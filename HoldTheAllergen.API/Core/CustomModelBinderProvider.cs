using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.API.Core
{
    public class CustomModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return modelType == typeof(User)
                       ? new UserModelBinder()
                       : null;
        }
    }
}