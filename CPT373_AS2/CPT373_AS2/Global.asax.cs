using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using CPT373_AS2.Models;

namespace CPT373_AS2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly string ActiveGamesKey = "ActiveGames";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // http://stackoverflow.com/questions/18954830/mvc4-how-to-hook-the-onsessionstart-event

        protected void Session_Start(object sender, EventArgs e)
        {
            // event is raised each time a new session is created     
            Session[ActiveGamesKey] = new UserActiveGames();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // event is raised when a session is abandoned or expires
        }
    }
}
