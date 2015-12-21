using BW.Common.Consts;
using BW.Common.Enums;
using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using BW.Website.Common.Constants;
using BW.Website.Common.Utilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Security;
using System.Linq;
using System.Security.Principal;
using System.Collections.Specialized;
using System.Globalization;
using BW.Website.Resource;

namespace BW.Website.Common.Helpers
{
    public static class AuthorizationHelper
    {
        public static int? UserId
        {
            get
            {
                if (!IsLogged)
                {
                    return null;
                }

                return CurrentUser.UserId;
            }
        }
        public static string Email
        {
            get
            {
                if (!IsLogged)
                {
                    return string.Empty;
                }

                return CurrentUser.Email;
            }
        }
        public static string FirstName
        {
            get
            {
                if (!IsLogged)
                {
                    return string.Empty;
                }

                return CurrentUser.FirstName;
            }
        }
        public static string LastName
        {
            get
            {
                if (!IsLogged)
                {
                    return string.Empty;
                }

                return CurrentUser.LastName;
            }
        }
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

        public static ResponeMessage<AuthenticationInfoDTO> Login(LoginInfoDTO login)
        {
            HttpResponseMessage response = ApiServiceUtilities.GetReponse(string.Format("api/UserApi/login/?email={0}&password={1}", login.Email, login.Password));
            var result = new ResponeMessage<AuthenticationInfoDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new AuthenticationInfoDTO() };
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<ResponeMessage<AuthenticationInfoDTO>>().Result;

                if (result.Code != ErrorCodeEnum.SUCCESS)
                {
                    return result;
                }

                SignOut();
                SessionManager.Instance.AddItem(SessionKeys.USERINFO, TransToStateServerEntity(result.Data));
            }
            return result;
        }

        public static ErrorCodeEnum IsAuthorized(IPrincipal user, string controller, string action,
            NameValueCollection queryString, string httpMethod, AuthorizedUserAttribute authAttr)
        {
            var strUrl = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", controller, action);
            if (authAttr != null && authAttr.IsMatch(PermissionCodes.AllowAnonymous.ToString()))
            {
                return ErrorCodeEnum.ACCEPTED;
            }

            if (!IsLogged)
            {
                return ExceptionHelper.RaiseError(ErrorCodeEnum.UNAUTHORIZED, GlobalResource.responseMessageUnAuthorized);
            }

            var permissionAccess = CheckAuthorized(authAttr, controller, action, httpMethod);
            if (permissionAccess == PermissionCodes.NotAllow)
            {
                return ExceptionHelper.RaiseError(ErrorCodeEnum.UNAUTHORIZED, GlobalResource.responseMessageUnAuthorized);
            }

            return ErrorCodeEnum.SUCCESS;
        }
        public static bool SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            SessionManager.Instance.Dispose();
            return true;
        }
        /// <summary>
        /// Check Current User Logged has permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool HasPermissionFor(string permission)
        {
            var currentUsr = CurrentUser;
            if (currentUsr == null || currentUsr.Permissions == null)
            {
                return false;
            }

            return CurrentUser.Permissions.Any(p => p.PermissionCode == permission);
        }

        public static bool HasPermissionFor(params string[] permissions)
        {
            var curr = CurrentUser;
            if (curr == null)
            {
                return false;
            }

            foreach (string item in permissions)
            {
                if (curr.Permissions.Any(p => p.PermissionCode == item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check Current User Logged has list of permission
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public static bool HasPermissionFor(List<string> permissions)
        {
            var currentUsr = CurrentUser;
            if (currentUsr == null || currentUsr.Permissions == null)
            {
                return false;
            }

            return currentUsr.Permissions.Any(p => permissions.Any(p1 => p.PermissionCode == p1));
        }

        #region Private method
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
        #endregion
    }
}
