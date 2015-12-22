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
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { Controller = "Home", action = ConstActionMethods.LOGIN }), true);
            base.OnException(filterContext);
        }
    }
}