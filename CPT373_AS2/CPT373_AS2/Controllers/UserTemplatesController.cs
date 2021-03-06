﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPT373_AS2.Models;

namespace CPT373_AS2.Controllers
{

    [CustomAuthorize]
    public class UserTemplatesController : Controller
    {
        private GOLDBEntities db = new GOLDBEntities();

        //private ApplicationUserManager manager;

        // GET: UserTemplates
        [AllowAnonymous]
        public ActionResult Index(string searchString)
        {
            var userTemplates = db.UserTemplates.Include(u => u.User);


            if (!String.IsNullOrEmpty(searchString))
            {
                userTemplates = userTemplates.Where(s => s.Name.Contains(searchString));
            }
            return View(userTemplates.ToList());
        }

        public ActionResult ListUserTemplates()
        {
            string sessionUserName = Session["UserName"].ToString();

            var user = db.Users.
                Where(u => u.Email == sessionUserName).
                First();

            //var userTemplates = db.UserTemplates.Include(u => u.User);
            var userTemplates = db.UserTemplates.
                Where(u => u.UserID == user.UserID).Include(u => u.User);
            return View(userTemplates.ToList());
        }

        // GET: UserTemplates/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTemplate userTemplate = db.UserTemplates.Find(id);
            if (userTemplate == null)
            {
                return HttpNotFound();
            }
            return View(userTemplate);
        }

        // GET: UserTemplates/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email");
            return View();
        }

        // POST: UserTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            UserTemplate userTemplate)
        {

                // TODO:
                // check the session is still valid
                // find the matching User in the DB
                // associate the Template with the User in the DB
                // by adding the Template to the User object Template
                // Collection

                //if (Session["Name"] != null)
                //{

                    if (ModelState.IsValid)
                    {
                        db.UserTemplates.Add(userTemplate);

                        string sessionUserName = Session["UserName"].ToString();


                        var user = db.Users.
                            Where(u => u.Email == sessionUserName).
                            First();

                        if (user != null)
                        {
                            user.UserTemplates.Add(userTemplate);
                            db.Entry(user).State = EntityState.Modified;
                        }
                       

                        db.SaveChanges();


                        return RedirectToAction("Index");

                    }
            //}

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userTemplate.UserID);
            return View(userTemplate);
        }

        // GET: UserTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTemplate userTemplate = db.UserTemplates.Find(id);
            if (userTemplate == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userTemplate.UserID);
            return View(userTemplate);
        }

        // POST: UserTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserTemplateID,UserID,Name,Height,Width,Cells")] UserTemplate userTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userTemplate.UserID);
            return View(userTemplate);
        }

        // GET: UserTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTemplate userTemplate = db.UserTemplates.Find(id);
            if (userTemplate == null)
            {
                return HttpNotFound();
            }
            return View(userTemplate);
        }

        // POST: UserTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTemplate userTemplate = db.UserTemplates.Find(id);
            string sessionUserName = Session["UserName"].ToString();
            var user = db.Users.
                Where(u => u.Email == sessionUserName).
                First();

            if (sessionUserName != null && user != null)
            {
                if (user.UserID == userTemplate.UserID)
                {
                    db.UserTemplates.Remove(userTemplate);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();



        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
