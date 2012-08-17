using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HoldTheAllergen.Backend.Core;
using HoldTheAllergen.Backend.Core.Attributes;

namespace HoldTheAllergen.Backend.Controllers
{
    [FilterIP(AllowedIPs = new[] { "::1", "99.37.64.29" })]
    public class DefaultController : Controller
    {
        private IMappingEngine _mappingEngine;

        public IMappingEngine MappingEngine
        {
            get { return _mappingEngine ?? (_mappingEngine = IoCHelper.GetInstance<IMappingEngine>()); }
        }

        protected FormActionResult<TForm> Form<TForm>(TForm model, Func<ActionResult> successContinuation,
                                                      Func<ActionResult> failureContinuation)
        {
            return new FormActionResult<TForm>(model, successContinuation, failureContinuation);
        }

        public ActionResult InvokeHttpError(HttpContextBase httpContext, HttpStatusCode errorCode)
        {
            //var pageRepository = IoCHelper.GetInstance<IPageRepository>();
            //var errorPage = pageRepository.Query(page => page.SystemCode == (int)errorCode).FirstOrDefault();

            //IController pageController = IoCHelper.GetInstance<PageController>();

            //var errorRoute = new RouteData();
            //errorRoute.Values.Add("controller", errorPage.ControllerName);
            //errorRoute.Values.Add("action", errorPage.ActionName);
            //errorRoute.DataTokens.Add("area", "cms");
            //errorRoute.Values.Add("errorCode", (int)errorCode);
            //errorRoute.Values.Add("id", errorPage.Id);
            //errorRoute.Values.Add("url", httpContext.Request.Url != null ? httpContext.Request.Url.OriginalString : "");

            //pageController.Execute(new RequestContext(httpContext, errorRoute));

            //httpContext.Response.StatusCode = (int)errorCode;
            //httpContext.Response.TrySkipIisCustomErrors = true;

            return new EmptyResult();
        }
    }
}