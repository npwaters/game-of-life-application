﻿using System;
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
        private UserActiveGames ActiveGames { get; set; }


        // GET: UserGames
        public ActionResult Index()
        {
            var userGames = db.UserGames.Include(u => u.User);
            return View(userGames);

        }



        public ActionResult ListSavedGames()
        {
            string sessionUserName = Session["UserName"].ToString();

            var user = db.Users.
                Where(u => u.Email == sessionUserName).
                First();

            //var userTemplates = db.UserTemplates.Include(u => u.User);
            var userGames = db.UserGames.
                Where(u => u.UserID == user.UserID).Include(u => u.User);
            return View(userGames.ToList());
        }

        // TODO:
        // we need an Action that allows the user to configure the Game
        // the Action should return a view that displays template details
        // as well as the edit Game form

        public ActionResult ConfigureGame(int? id)
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


        [AllowAnonymous]
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
            TempData["template"] = template;
            return View(viewModel);
        }


        // POST: UserGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "UserGameID,UserID,Name,Height,Width,Cells")] UserGame userGame)
        {


            //if (Session.Keys.Count == 0)
            //{
            //    ActiveUserGameList = new List<UserGame>();
            //}


            //ActiveUserGameList.Add(userGame);

            // TODO:
            // retrieve x and y coords from the Form Request
            // to use as parameters for InsertTemplate()

            int x = Int32.Parse(Request.Form["Xcoord"]);
            int y = Int32.Parse(Request.Form["YCoord"]);

            // grab the template cells from TempData
            UserTemplate template = TempData["template"] as UserTemplate;


            // intialise the Game cells
            userGame.initialiseCells();


            // TODO:
            // call InsertTemplate() (either in Game or here)

            userGame.InsertTemplate(template, x, y);

            


            if (ModelState.IsValid)
            {

                // add the Game to the session
                ActiveGames = Session[MvcApplication.ActiveGamesKey] as UserActiveGames;
                ActiveGames.AddGame(userGame);

                // redirect to a list of active Games (session)
                return RedirectToAction("ListActiveGames");


                // pass the Game to NewGameDetails() Action
                // https://msdn.microsoft.com/en-us/library/dd394711(v=vs.100).aspx
                //TempData["game"] = userGame;
                //return RedirectToAction("NewGameDetails");
                //return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", userGame.UserID);
            return View(userGame);
        }

        public void UpdateStoppedSessionGame(UserGame game)
        {
            ActiveGames = Session[MvcApplication.ActiveGamesKey] as UserActiveGames;
            var g = ActiveGames.findGame(game.UserGameSessionID);
            g.Cells = game.Cells;
        }


        public ActionResult RetrieveActiveGame(int? id)
        {
            var userGames = Session["ActiveGames"] as UserActiveGames;
            var g = ActiveGames.findGame(id);
            //g = TempData["game"] as UserGame;
            //SaveGame(g);
            return RedirectToAction("SaveGame");
        }




        [CustomAuthorize]
        public ActionResult SaveGame
            (int? id)
        {


            if (Session["UserName"] != null)
            {
                if (ModelState.IsValid)
                {

                    var userGames = Session["ActiveGames"] as UserActiveGames;
                    var g = userGames.findGame(id);
                    db.UserGames.Add(g);

                    string sessionUserName = Session["UserName"].ToString();


                    var user = db.Users.
                        Where(u => u.Email == sessionUserName).
                        First();

                    if (user != null)
                    {
                        user.UserGames.Add(g);
                        db.Entry(user).State = EntityState.Modified;
                    }
                    db.SaveChanges();



                    return RedirectToAction("ListSavedGames");
                    //return RedirectToAction("Index");
                }
            }

            return View();
        }

        public ActionResult PlayActiveGame(int? id)
        {
            var userGames = Session["ActiveGames"] as UserActiveGames;
            var game = userGames.findGame(id);
            return View(game);
        }

        public ActionResult NewGameDetails()
        {
            UserGame game = TempData["game"] as UserGame;

            // TODO:
            // create a HTML helper extension to display the Game with Cells
            // in the NewGameDetails View
            return View(game);
        }

        public ActionResult ListActiveGames()
        {
            var userGames = Session["ActiveGames"] as UserActiveGames;
            return View(userGames.getActiveGames());
        }

        [CustomAuthorize]
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
        [CustomAuthorize]
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
