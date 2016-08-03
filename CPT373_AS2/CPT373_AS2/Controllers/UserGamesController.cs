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

        private GOLDBEntities db = new GOLDBEntities();
        //List<UserGame> ActiveUserGameList;
        private UserActiveGames ActiveGames { get; set; }


        // GET: UserGames
        public ActionResult Index()
        {
            //var userGames = db.UserGames.Include(u => u.User);
            //return View(ActiveGames.getActiveGames());
            var userGames = Session["ActiveGames"] as UserActiveGames;
            return View(userGames.getActiveGames());

            //return View(Session["ActiveGames"] as List<UserGame>);
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


        //public ActionResult Create()
        //{
        //    var viewModel = new GOLViewModel
        //    {
        //        UserTemplates = null,
        //        UserGame = new UserGame()
        //    };

        //    return View();
        //}

        public ActionResult Create(int? id)
        {
            var template = db.UserTemplates.Find(id);

            // variables for X and Y position of template


            if (template == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GOLViewModel
            {
                UserTemplates = template,
                UserGame = new UserGame()
            };

            //ViewDataDictionary GolVDD = new ViewDataDictionary(viewModel);


            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email");
            ViewBag.TemplateHeight = template.Height;
            ViewBag.TemplateWidth = template.Width;
            return View(viewModel);
        }


        // POST: UserGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "UserGameID,UserID,Name,Height,Width,Cells")] UserGame userGame)
            //[Bind(Include = "XCoord")] int? x,
            //[Bind(Include = "YCoord")] int? y)
        {

            // manage the session via session keys
            ActiveGames = Session[MvcApplication.ActiveGamesKey] as UserActiveGames;
            ActiveGames.AddGame(userGame);
            //if (Session.Keys.Count == 0)
            //{
            //    ActiveUserGameList = new List<UserGame>();
            //}


            //ActiveUserGameList.Add(userGame);

            // TODO:
            // retrieve x and y coords from the Form Request

            string x = Request.Form["Xcoord"];
            string y = Request.Form["YCoord"];

            // TODO:
            // call createTemplate (either in Game or here)

            // TODO:
            // add the Game to the session
            
            //Session["ActiveUserGameList"] = ActiveUserGameList;
            //ActiveUserGameList.Add(userGame);
            //var games = Session["ActiveUserGameList"] as List<UserGame>;

            // TODO:
            // move the save to DB code below to a 'SaveGame' action
            if (ModelState.IsValid)
            {
                db.UserGames.Add(userGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userGame.UserID);
            return View(userGame);
        }


        public ActionResult SaveGame()
        {
            return View();
        }

        public ActionResult PlayGame()
        {
            return View();
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
