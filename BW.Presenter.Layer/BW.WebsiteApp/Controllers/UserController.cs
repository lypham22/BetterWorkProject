﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW.Website.Common.Helpers;
using BW.Data.Contract.DTOs;

namespace BW.WebsiteApp.Controllers
{
    public class UserController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var getAllUser = UserHelper.GetAllUser();
            return View(getAllUser);
        }

        // GET: Users/Details/5
        //public ActionResult Details(string userId)
        //{
        //    var result = UserHelper.GetUserById(userId);
        //    return View(result);
        //}

        // GET: Users/Create
        public ActionResult Create()
        {
            UserCreateView dto = new UserCreateView();
            dto.roles = RoleHelper.GetAllRole();
            return View(dto);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateView userCreateView, string[] groupRole)
        {
            if (ModelState.IsValid)
            {
                var result = UserHelper.InsertUser(userCreateView, groupRole);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(userCreateView);
        }

        // GET: Users/Edit/5
        //public ActionResult Edit(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var result = UserHelper.GetUserById(userId);
        //    if (result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(result);
        //}

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(UserEditView userView)
        //{
        //    //var errors = ModelState.Values.SelectMany(v => v.Errors);
        //    if (ModelState.IsValid)
        //    {
        //        var result = UserHelper.UpdateUser(userView);
        //        if (result)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(userView);
        //}

        // GET: Users/Delete/5
        //public ActionResult Delete(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var result = UserHelper.GetUserById(userId);
        //    if (result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(result);
        //}

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int userId)
        //{
        //    UserHelper.DeleteUser(userId);
        //    return RedirectToAction("Index");
        //}
    }
}
