using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW.WebsiteApp.Models;
using BW.Website.Common.Helpers;
using BW.Data.Contract.DTOViews;

namespace BW.WebsiteApp.Controllers
{
    public class UserController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var getAllUser = ProductHelper.GetAllUser();
            return View(getAllUser);
        }

        // GET: Users/Details/5
        public ActionResult Details(string userId)
        {
            var result = ProductHelper.GetUserById(userId);
            return View(result);
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
        public ActionResult Create(UserView userView)
        {
            if (ModelState.IsValid)
            {
                var result = ProductHelper.UpdateUser(userView);
                if (result) 
                { 
                    return RedirectToAction("Index");
                }
            }

            return View(userView);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }
            else
            {
                var result = ProductHelper.GetUserById(userId);
                return View(result);
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserView userView)
        {
            if (ModelState.IsValid)
            {
                var result = ProductHelper.UpdateUser(userView);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(userView);
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
    }
}
