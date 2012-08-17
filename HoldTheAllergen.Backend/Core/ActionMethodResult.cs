using System;
using System.Web.Mvc;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Backend.Core
{
    public interface IActionMethodResult
    {
    }

    public class ActionMethodResultInvokerFacade<T> : IActionMethodResultInvoker
        where T : IActionMethodResult
    {
        private readonly IActionMethodResultInvoker<T> _invoker;

        public ActionMethodResultInvokerFacade(IActionMethodResultInvoker<T> invoker)
        {
            _invoker = invoker;
        }

        #region IActionMethodResultInvoker Members

        public ActionResult Invoke(object actionMethodResult, ControllerContext context)
        {
            return _invoker.Invoke((T) actionMethodResult, context);
        }

        #endregion
    }

    public class FormActionResult<TModel> : IActionMethodResult
    {
        public FormActionResult(TModel model,
                                Func<ActionResult> successContinuation,
                                Func<ActionResult> failureContinuation)
        {
            Model = model;
            SuccessContinuation = successContinuation;
            FailureContinuation = failureContinuation;
        }

        public TModel Model { get; private set; }
        public Func<ActionResult> SuccessContinuation { get; private set; }
        public Func<ActionResult> FailureContinuation { get; private set; }
    }

    public class CommandMethodResultInvoker<TModel> : IActionMethodResultInvoker<FormActionResult<TModel>>
    {
        private readonly IFormHandler<TModel> _handler;

        public CommandMethodResultInvoker(IFormHandler<TModel> handler)
        {
            _handler = handler;
        }

        #region IActionMethodResultInvoker<FormActionResult<TModel>> Members

        public ActionResult Invoke(FormActionResult<TModel> actionMethodResult, ControllerContext context)
        {
            if (!context.Controller.ViewData.ModelState.IsValid) return actionMethodResult.FailureContinuation();

            _handler.Handle(actionMethodResult.Model);

            return actionMethodResult.SuccessContinuation();
        }

        #endregion
    }

    public interface IActionMethodResultInvoker<in T>
        where T : IActionMethodResult
    {
        ActionResult Invoke(T actionMethodResult, ControllerContext context);
    }

    public interface IActionMethodResultInvoker
    {
        ActionResult Invoke(object actionMethodResult, ControllerContext context);
    }
}