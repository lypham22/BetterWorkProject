﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW.WebsiteApp.Models;
using BW.Website.Common.Helpers;
using BW.Data.Contract.DTOs_View;

namespace BW.WebsiteApp.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var getAllUser = ProductHelper.GetAllUser();
            return View(getAllUser);
        }

        
        public ActionResult SearchUser(int userid)
       {
           var SearchUser = ProductHelper.GetUser(userid);
            return View(SearchUser);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = db.Users.Find(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(user);
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInfo user)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Users.Add(user);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            user.Name = "lypham test";


            var SearchUser = ProductHelper.CreateUser(user);
            //return View(user);
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = db.Users.Find(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(user);
            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Password,Name,Role")] User user)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(user).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(user);
            return View();
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = db.Users.Find(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(user);
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //User user = db.Users.Find(id);
            //db.Users.Remove(user);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}
