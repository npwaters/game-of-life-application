using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPT373_AS2.Models
{
    public class UserActiveGames
    {
        private List<UserGame> ActiveGames { get; set; }

        public UserActiveGames()
        {
            ActiveGames = new List<UserGame>();
        }


        public void AddGame(UserGame game)
        {
            ActiveGames.Add(game);
        }

        public List<UserGame> getActiveGames()
        {
            return ActiveGames;
        }


    }
}