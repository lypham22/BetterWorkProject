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
            var getAllRoleInPermission = RoleInPermissionHelper.GetAllRoleInPermisson().Data;
            return View(getAllRoleInPermission);
        }
        // GET: Users/Edit/5
        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult Edit(string roleInPermIdEnc)
        {
            if (string.IsNullOrEmpty(roleInPermIdEnc))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = RoleInPermissionHelper.GetRoleInPermissionById(roleInPermIdEnc).Data;
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult Edit(RoleInPermissonView roleInPermissonView)
        {
            if (ModelState.IsValid)
            {
                var result = RoleInPermissionHelper.UpdateRoleInPermission(roleInPermissonView).Data;
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(roleInPermissonView);
        }

        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult ViewAll()
        {
            var getAllRoleInPermission = RoleInPermissionHelper.GetAllRoleInPermisson().Data;
            return View(getAllRoleInPermission);
        }

        [AuthorizedUser(PermissionCodes.AllowAnonymous)]
        public ActionResult Save(string roleInPermIdEnc, string roleInPermAddEnc,
            string roleInPermEditEnc, string roleInPermDelEnc, string roleInPermViewEnc)
        {

            var result = RoleInPermissionHelper.UpdateRoleInPermissionJSCript(roleInPermIdEnc, roleInPermAddEnc,
            roleInPermEditEnc, roleInPermDelEnc, roleInPermViewEnc).Data;
            if (result)
            {
                return RedirectToAction("ViewAll");
            }

            return View(result);
        }
        //[AuthorizedUser(PermissionCodes.AllowAnonymous)]
        //public ActionResult Save(string roleInPermIdEnc, string roleInPermAddEnc, string roleInPermEditEnc, string roleInPermDelEnc, string roleInPermViewEnc)
        //{
        //    var result = RoleInPermissionHelper.UpdateRoleInPermission(roleInPermIdEnc, roleInPermAddEnc, roleInPermEditEnc, roleInPermDelEnc, roleInPermViewEnc);
        //    if (result != null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(result);
        //}

        // GET: Users/Details/5
        //[AuthorizedUser(PermissionCodes.AllowAnonymous)]
        //public ActionResult Details(string roleId)
        //{
        //    var result = RoleHelper.GetRoleById(roleId).Data;
        //    return View(result);
        //}

        // GET: Users/Create
        //[AuthorizedUser(PermissionCodes.AllowAnonymous)]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AuthorizedUser(PermissionCodes.AllowAnonymous)]
        //public ActionResult Create(RoleCreateView roleCreateView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = RoleHelper.InsertRole(roleCreateView);
        //        if (result.Data)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(roleCreateView);
        //}

        //GET: Users/Delete/5
        //[AuthorizedUser(PermissionCodes.AllowAnonymous)]
        //public ActionResult Delete(string roleId)
        //{
        //    if (string.IsNullOrEmpty(roleId))
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var result = RoleHelper.GetRoleById(roleId).Data;
        //    if (result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(result);
        //}

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[AuthorizedUser(PermissionCodes.AllowAnonymous)]
        //public ActionResult DeleteConfirmed(int roleId)
        //{
        //    RoleHelper.DeleteRole(roleId);
        //    return RedirectToAction("Index");
        //}
    }
}
