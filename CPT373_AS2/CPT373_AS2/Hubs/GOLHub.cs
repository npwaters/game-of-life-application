using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

using CPT373_AS2.Models;

namespace CPT373_AS2
{
    public class GolHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }


        // TODO:
        // implement all playgame code in the Hub??


        public void PlayActiveGame(UserGame game)
        {
            //Clients.Caller.addTurnToPage(cells);
        }
    }
}