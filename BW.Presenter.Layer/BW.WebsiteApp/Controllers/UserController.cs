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
    public class UserController : Controller
    {
        [AuthorizedUser(PermissionCodes.ViewManageUser)]
        public ActionResult Index()
        {
            
            
            var getAllUser = UserHelper.GetUserById("1").Data;
            return View(getAllUser);
        }

        // GET: Users/Details/5
        [AuthorizedUser(PermissionCodes.ViewManageUser)]
        public ActionResult Details(string userId)
        {
            var result = UserHelper.GetUserById(userId).Data;
            return View(result);
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
        public ActionResult Edit(UserCreateView userView, string[] groupRole)
        {
            //UserCreateView userView = null;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                //userView = ToUserCreaView(userDTOs);
                var result = UserHelper.UpdateUser(userView, groupRole);
                if (result.Data)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(userView);
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
        public ActionResult DeleteConfirmed(int userId)
        {
            UserHelper.DeleteUser(userId);
            return RedirectToAction("Index");
        }
    }
}
