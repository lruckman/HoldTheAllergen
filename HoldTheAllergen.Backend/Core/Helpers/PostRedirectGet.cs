using System.Web.Mvc;

namespace HoldTheAllergen.Backend.Core.Helpers
{
    public class PostRedirectGet
    {
        public class ExportModelState : ModelStateTempDataTransfer
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                //Only export when ModelState is not valid
                if (!filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    //Export if we are redirecting
                    if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
                    {
                        filterContext.Controller.TempData[Key] = filterContext.Controller.ViewData.ModelState;
                    }
                }

                base.OnActionExecuted(filterContext);
            }
        }

        public abstract class ModelStateTempDataTransfer : ActionFilterAttribute
        {
            protected static readonly string Key = typeof (ModelStateTempDataTransfer).FullName;
        }

        public class ImportModelState : ModelStateTempDataTransfer
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                var modelState = filterContext.Controller.TempData[Key] as ModelStateDictionary;

                if (modelState != null)
                {
                    //Only Import if we are viewing
                    if (filterContext.Result is ViewResult || filterContext.Result is PartialViewResult)
                    {
                        filterContext.Controller.ViewData.ModelState.Merge(modelState);
                    }
                    else
                    {
                        //Otherwise remove it.
                        filterContext.Controller.TempData.Remove(Key);
                    }
                }

                base.OnActionExecuted(filterContext);
            }
        }
    }
}