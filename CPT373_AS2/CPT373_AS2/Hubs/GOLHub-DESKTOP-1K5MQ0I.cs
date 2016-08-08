using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

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


        public void PlayActiveGame(string cells)
        {

        }
    }
}