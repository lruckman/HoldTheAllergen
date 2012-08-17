using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace HoldTheAllergen.Backend.Core
{
    public class InjectingActionInvoker : ControllerActionInvoker
    {
        private readonly IContainer _container;

        public InjectingActionInvoker(IContainer container)
        {
            _container = container;
        }

        protected override FilterInfo GetFilters(
            ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var info = base.GetFilters(controllerContext, actionDescriptor);

            (info.AuthorizationFilters as List<IAuthorizationFilter>).ForEach(_container.BuildUp);
            (info.ActionFilters as List<IActionFilter>).ForEach(_container.BuildUp);
            (info.ResultFilters as List<IResultFilter>).ForEach(_container.BuildUp);
            (info.ExceptionFilters as List<IExceptionFilter>).ForEach(_container.BuildUp);

            return info;
        }

        protected override ActionResult CreateActionResult(
            ControllerContext controllerContext,
            ActionDescriptor actionDescriptor,
            object actionReturnValue)
        {
            if (actionReturnValue is IActionMethodResult)
            {
                var openWrappedType = typeof (ActionMethodResultInvokerFacade<>);
                var actionMethodResultType = actionReturnValue.GetType();
                var wrappedResultType = openWrappedType.MakeGenericType(actionMethodResultType);

                var invokerFacade = (IActionMethodResultInvoker) _container.GetInstance(wrappedResultType);

                var result = invokerFacade.Invoke(actionReturnValue, controllerContext);

                return result;
            }
            return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
        }
    }
}