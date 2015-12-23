using BW.Data.Contract.DTOs;
using BW.Website.Common.Constants;
using BW.Website.Common.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace BW.Website.Common
{
    public class BWHandleError : HandleErrorAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var model = new BWHandleErrorInfo();
            model.InnerException = filterContext.Exception;
            model.FriendlyErrorMessage = filterContext.Exception.Message;
            model.StackTraceMessage = model.InnerException.ToString();

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                MasterName = "~/Views/Shared/_LoginLayout.cshtml",
                ViewData = new ViewDataDictionary<BWHandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };
            base.OnException(filterContext);
        }
    }
}