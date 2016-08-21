using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPT373_AS2.Models;
using CryptSharp;

namespace CPT373_AS2.Controllers
{
    public class AccountController : Controller
    {
        private GOLDBEntities db = new GOLDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,Email,Password,FirstName,LastName,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                using (GOLDBEntities database = new GOLDBEntities())
                {
                    // Retrieve a user with the same username and password.
                    User login = database.Users.FirstOrDefault(u => u.Email == user.Email &&
                                                                    u.Password == user.Password);

                    // If successful set the session variables and go to Member page.
                    if (login == null)
                    {
                        Session["Username"] = user.Email;
                        Session["Name"] = user.FirstName;

                        user.IsAdmin = false;
                        user.Password = Crypter.Blowfish.Crypt(user.Password);
                        db.Users.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");

                    }
                }

                
            }
            return View(user);
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your login page.";

            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPost([Bind(Include = "Email,Password")] User user)
        {
            /* UsersEntities is the name provided when you created your model
             * if you changed yours during the creation of the model, then you 
             * will find the name of the entity in your Web.config file in the 
             * connection string. This object represents your database.*/
            using (GOLDBEntities database = new GOLDBEntities())
            {
                // Retrieve a user with the same username and password.
                //User login = database.Users.FirstOrDefault(u => u.Email == user.Email &&
                //                                                u.Password == user.Password);

                User login = database.Users.FirstOrDefault(u => u.Email == user.Email);


                // If successful set the session variables and go to Member page.
                if (login != null && Crypter.CheckPassword(user.Password, login.Password))
                {
                    Session["Username"] = login.Email;
                    Session["Name"] = login.FirstName;

                    return RedirectToAction("Index", "Home");
                }
            }

            // Otherwise return them to the login page
            return View(user);
        }

        public ActionResult Logout()
        {
            // Kill the session variables & return to login page
            Session["Username"] = null;
            Session["Name"] = null;

            return RedirectToAction("Login");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Email,Password,FirstName,LastName,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
