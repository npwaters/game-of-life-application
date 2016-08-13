using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using CPT373_AS2.Models;

namespace CPT373_AS2.Hubs
{
    [HubName("golTicker")]

    public class GolTickerHub : Hub
    {
        private readonly GolTicker _golTicker;

        public GolTickerHub() :
            this(GolTicker.Instance)
        {

        }

        public GolTickerHub(GolTicker golTicker)
        {
            _golTicker = golTicker;
        }

        public void PlayActiveGame(UserGame game)
        {
            _golTicker.StartGame(game);
        }

    }
}