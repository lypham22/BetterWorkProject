using BW.Common.Consts;
using BW.Common.Enums;
using BW.Data.Contract.DTOs;
using BW.Website.Common.Constants;
using BW.Website.Common.Helpers;
using BW.Website.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BW.WebsiteApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult Index()
        {
            var currentUser = AuthorizationHelper.CurrentUser;
            if (AuthorizationHelper.IsLogged && currentUser != null)
            {
                return RedirectToAction(ConstActionMethods.INDEX, ConstActionMethods.CTL_USER);
            }
            else
            {
                return RedirectToAction(ConstActionMethods.LOGIN);
            }
        }

        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult Login()
        {
            return View();
        }

        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        [HttpPost]
        public ActionResult Login(LoginInfoDTO login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var result = AuthorizationHelper.Login(login);
            if (result.Code == ErrorCodeEnum.SUCCESS)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View(login);
            }
            
        }
        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult Logout()
        {
            if (!AuthorizationHelper.IsLogged)
            {
                return RedirectToAction(ConstActionMethods.INDEX);
            }
            AuthorizationHelper.SignOut();
            return RedirectToAction(ConstActionMethods.INDEX);
        }
    }
}