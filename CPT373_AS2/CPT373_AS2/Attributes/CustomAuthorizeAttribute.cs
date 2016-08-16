using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class CustomAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        /* Put your authorisation check here.
         * Note: This is checking the session variable, so
         * once the user has logged in they remain logged in
         * until they log out, you programatically kill the session
         * or the session expires (timesout). */
        return httpContext.Session["Username"] != null;
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        // Put your redirect to login controller here.
        filterContext.Result = new RedirectToRouteResult(
                               new RouteValueDictionary(new { controller = "Account", action = "Login" }));
    }
}
