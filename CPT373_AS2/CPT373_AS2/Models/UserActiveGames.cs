using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPT373_AS2.Models
{
    public class UserActiveGames
    {
        int sessionID = 0;

        private List<UserGame> ActiveGames { get; set; }

        public UserActiveGames()
        {
            ActiveGames = new List<UserGame>();
        }


        public void AddGame(UserGame game)
        {
            game.UserGameSessionID = sessionID;
            ActiveGames.Add(game);
            // TODO:
            // not sure if incrementing the sessionID here is corrcet
            sessionID++;
        }

        public void removeGame(UserGame game)
        {
            ActiveGames.Remove(game);
        }

        public List<UserGame> getActiveGames()
        {
            return ActiveGames;
        }

        public UserGame findGame(int? sessionID)
        {

            return ActiveGames.Find(p => p.UserGameSessionID == sessionID);

            
        }


    }
}