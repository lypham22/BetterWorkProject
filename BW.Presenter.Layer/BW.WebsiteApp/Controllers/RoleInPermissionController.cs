using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW.Website.Common.Helpers;
using BW.Data.Contract.DTOs;
using BW.Website.Common.Utilities;
using BW.Common.Consts;

namespace BW.WebsiteApp.Controllers
{
    public class RoleInPermissionController : Controller
    {
        // GET: RoleInPermission
        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowData(string roleIdEnc)
        {
            var result = RoleInPermissionHelper.GetRoleInPermissionByRoleId(roleIdEnc).Data;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PermissionsPartialView", result);
            }
            else
            {
                return View();
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult SaveChange(List<RoleInPermissonView> data)
        {
            var result = RoleInPermissionHelper.UpdateRoleInPermission(data).Data;
            return View(result);
        }
    }
}
