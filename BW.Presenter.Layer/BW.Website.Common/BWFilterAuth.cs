using System;
using System.Globalization;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http.Controllers;
using BW.Website.Common.Utilities;
using BW.Common.Enums;
using BW.Website.Common.Helpers;

namespace BW.Website.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BWValidateAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext authorizationContext)
        {
            var controller = authorizationContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = authorizationContext.ActionDescriptor.ActionName;
            var user = authorizationContext.HttpContext.User;

            object[] authAttrs = null;
            authAttrs = authorizationContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizedUserAttribute), true);
            if (authAttrs.Length == 0)
            {
                authAttrs = authorizationContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AuthorizedUserAttribute), true);
            }

            if (AuthorizationHelper.IsAuthorized(user, controller, action, authorizationContext.HttpContext.Request.QueryString,
                authorizationContext.RequestContext.HttpContext.Request.HttpMethod, authAttrs.Length > 0 ? (AuthorizedUserAttribute)authAttrs[0] : null) != ErrorCodeEnum.SUCCESS)
            {
                return;
            }

            if (string.Compare(authorizationContext.RequestContext.HttpContext.Request.HttpMethod, HttpVerbs.Post.ToString().ToUpper()) != 0)
            {
                return;
            }

        }

    }
}