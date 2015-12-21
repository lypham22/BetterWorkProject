using BW.Common.Consts;
using BW.Common.Enums;
using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using BW.Website.Common.Constants;
using BW.Website.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Security;
using System.Linq;
using System.Security.Principal;
using System.Collections.Specialized;

namespace BW.Website.Common.Helpers
{
    public static class HomeHelper
    {
        public static bool IsLogged
        {
            get
            {
                bool isLogged = SessionManager.Instance.GetData<AuthenticationInfoStateServer>(SessionKeys.USERINFO) != null;
                return isLogged;
            }
        }

        public static AuthenticationInfoStateServer CurrentUser
        {
            get
            {
                return SessionManager.Instance.GetData<AuthenticationInfoStateServer>(SessionKeys.USERINFO);
            }
        }
        public static ErrorCodeEnum IsAuthorized(IPrincipal user, string controller, string action,
            NameValueCollection queryString, string httpMethod, AuthorizedUserAttribute authAttr)
        {

            //if (result.Code != ErrorCodeEnum.SUCCESS)
            //        {
            //            return result;
            //        }
            //    }
            //    SignOut();
            //    SessionManager.Instance.AddItem(SessionKeys.USERINFO, TransToStateServerEntity(result.Data));
            //    return result;
            return new ErrorCodeEnum();
        }
        public static bool SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            SessionManager.Instance.Dispose();
            return true;
        }
        private static AuthenticationInfoStateServer TransToStateServerEntity(AuthenticationInfoDTO dto)
        {
            AuthenticationInfoStateServer sObj = new AuthenticationInfoStateServer();
            sObj.UserId = dto.UserId;
            sObj.Email = dto.Email;
            sObj.FirstName = dto.FirstName;
            sObj.LastName = dto.LastName;
            sObj.Permissions = new List<PermissionStateServer>();

            foreach (var item in dto.modules)
            {
                sObj.Permissions.Add(new PermissionStateServer
                {
                    PermissionCode = item.PermissionCode,
                });
            }

            return sObj;
        }
        private static string CheckAuthorized(AuthorizedUserAttribute authAttr, string controller, string action = "", string httpMethod = "GET")
        {
            var currentUsr = CurrentUser;

            // Not login
            if (currentUsr == null || currentUsr.Permissions == null)
            {
                return PermissionCodes.NotAllow;
            }

            if (authAttr == null)
            {
                return PermissionCodes.AllowAccess;
            }

            //Get permission code list
            var userPermissionCodeList = currentUsr.Permissions != null ? currentUsr.Permissions.Select(p => p.PermissionCode).ToList() : null;

            // view page
            if (authAttr.IsMatch(userPermissionCodeList))
            {
                return PermissionCodes.AllowAccess;
            }

            return PermissionCodes.NotAllow;
        }
    }
}
