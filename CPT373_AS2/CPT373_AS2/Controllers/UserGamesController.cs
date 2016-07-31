using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPT373_AS2.Models;
using CPT373_AS2.ViewModels;

namespace CPT373_AS2.Controllers
{
    public class UserGamesController : Controller
    {

        //private int? id;
        //static private UserTemplate ut;
        private GOLDBEntities db = new GOLDBEntities();

        // GET: UserGames
        public ActionResult Index()
        {
            var userGames = db.UserGames.Include(u => u.User);
            return View(userGames.ToList());
        }

        
        // TODO:
        // we need an Action that allows the user to configure the Game
        // the Action should return a view that displays template details
        // as well as the edit Game form

        public ActionResult ConfigureGame(int? id)
        {
            UserTemplate userTemplate = db.UserTemplates.Find(id);
            if (userTemplate == null)
            {
                return HttpNotFound();
            }

            return View(userTemplate);
        }
        
        
        
        // GET: UserGames/Details/5
        public ActionResult Details(int? id)
        {
            //this.id = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGame userGame = db.UserGames.Find(id);
            if (userGame == null)
            {
                return HttpNotFound();
            }
            return View(userGame);
        }

        // GET: UserGames/Create
        public ActionResult Create(int? id)
        {
            var template = db.UserTemplates.Find(id);

            //ut = db.UserTemplates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GOLViewModel
            {
                UserTemplates = template,
                UserGame = new UserGame()
            };

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email");
            return View(viewModel);
        }

        //public PartialViewResult RenderUserTemplate()
        //{

        //    return PartialView(ut);
        //}

        // POST: UserGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserGameID,UserID,Name,Height,Width,Cells")] UserGame userGame)
        {
            if (ModelState.IsValid)
            {
                db.UserGames.Add(userGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userGame.UserID);
            return View(userGame);
        }

        // GET: UserGames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGame userGame = db.UserGames.Find(id);
            if (userGame == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userGame.UserID);
            return View(userGame);
        }

        // POST: UserGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserGameID,UserID,Name,Height,Width,Cells")] UserGame userGame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userGame.UserID);
            return View(userGame);
        }

        // GET: UserGames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGame userGame = db.UserGames.Find(id);
            if (userGame == null)
            {
                return HttpNotFound();
            }
            return View(userGame);
        }

        // POST: UserGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserGame userGame = db.UserGames.Find(id);
            db.UserGames.Remove(userGame);
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
