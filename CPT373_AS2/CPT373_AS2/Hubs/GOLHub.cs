using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

using CPT373_AS2.Models;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

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
            StringBuilder output = new StringBuilder();
            output.Append("\u0020");
            output.Append("\u0020");
            output.Append("\u0020");
            output.AppendLine();
            output.Append('\u2588');
            output.Append('\u2588');
            output.Append('\u2588');
            output.AppendLine();
            output.Append("\u0020");
            output.Append("\u0020");
            output.Append("\u0020");


            game.Cells = "XXX\r\nOOO\r\nXXX";

            //Clients.Caller.addTurnToPage(JsonConvert.SerializeObject(game));
            //MvcHtmlString cells = new MvcHtmlString(output.ToString());

            string cells = output.ToString();

            Clients.Caller.addTurnToPage(cells);
            //Clients.Caller.addTurnToPage(JsonConvert.SerializeObject(cells));
        }
    }
}