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
using PagedList;

namespace BW.WebsiteApp.Controllers
{
    public class UserController : Controller
    {
        [AuthorizedUser(PermissionCodes.ViewManageUser)]
        public ActionResult Index(int? page)
        {
            var getAllUser = UserHelper.GetAllUser().Data;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(getAllUser.ToPagedList(pageNumber, pageSize));
        }

        // GET: Users/Details/5
        [AuthorizedUser(PermissionCodes.ViewManageUser)]
        public ActionResult Details(string userId)
        {
            //var result = UserHelper.GetUserById(ApiServiceUtilities.Encrypt(userId)).Data;
            var result = UserHelper.GetUserById(userId).Data;
            return View(result);
        }

        // GET: Users/Details/5
        [AuthorizedUser(PermissionCodes.ViewManageUser)]
        public ActionResult ViewProfile()
        {
            string userIdEnc = AuthorizationHelper.UserId.ToString();
            var result = UserHelper.GetUserById(ApiServiceUtilities.Encrypt(userIdEnc)).Data;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserProfilePartialView", result);
            }
            else
            {
                return View();
            }
        }

        // GET: Users/Create
        [AuthorizedUser(PermissionCodes.AddManageUser)]
        public ActionResult Create()
        {
            UserCreateView dto = new UserCreateView();
            dto.roles = RoleHelper.GetRoleActive().Data;
            return View(dto);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizedUser(PermissionCodes.AddManageUser)]
        public ActionResult Create(UserCreateView userCreateView, string[] groupRole)
        {
            if (ModelState.IsValid)
            {
                var result = UserHelper.InsertUser(userCreateView, groupRole);
                if (result.Data)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(userCreateView);
        }

        // GET: Users/Edit/5
        [AuthorizedUser(PermissionCodes.EditManageUser)]
        public ActionResult Edit(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = UserHelper.GetUserById(userId).Data;
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        [HttpPost]
        [AuthorizedUser(PermissionCodes.EditManageUser)]
        public ActionResult Edit(UserView userView, string[] groupRole)
        {
            if (ModelState.IsValid)
            {
                var result = UserHelper.UpdateUser(userView, groupRole);
                if (result.Data)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(userView);
        }

        // GET: Users/EditProfile/5
        [AuthorizedUser(PermissionCodes.EditManageUser)]
        public ActionResult EditProfile()
        {
            string userIdEnc = AuthorizationHelper.UserId.ToString();
            var result = UserHelper.GetUserById(ApiServiceUtilities.Encrypt(userIdEnc)).Data;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EditProfilePartialView", result);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AuthorizedUser(PermissionCodes.EditManageUser)]
        public string EditProfile(UserProfileView userProfileView)
        {
            if (ModelState.IsValid)
            {
                var result = UserHelper.EditProfile(userProfileView);
                if (result.Data)
                {
                    return "Update successful!";
                }
            }

            return "Update fail!";
        }

        [AuthorizedUser(PermissionCodes.DeleteManageUser)]
        public ActionResult Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = UserHelper.GetUserById(userId).Data;
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [AuthorizedUser(PermissionCodes.DeleteManageUser)]
        public ActionResult DeleteConfirmed(string userId)
        {
            UserHelper.DeleteUser(ApiServiceUtilities.Encrypt(userId));
            return RedirectToAction("Index");
        }

        [AuthorizedUser(PermissionCodes.EditManageUser)]
        public ActionResult UpdatePassword()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_UpdatePasswordPartialView");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public string UpdatePassword(UserPasswordView userPassView)
        {
            //check old password
            string userIdEnc = AuthorizationHelper.UserId.ToString();
            var user = UserHelper.GetUserById(ApiServiceUtilities.Encrypt(userIdEnc)).Data;
            if (!UserHelper.CheckOldPassword(user.Email, ApiServiceUtilities.MD5Hash(userPassView.OldPassword)))
            {
                return "Wrong current password!";
            }else
            {
                UserHelper.UpdatePassword(userPassView);
                return "Change password success!";
            }

        }
        //public JsonResult IsEmailExits(string email)
        //{
        //    return Json(!UserHelper.CheckUnitEmail(email).Data, JsonRequestBehavior.AllowGet);
        //}
    }
}
