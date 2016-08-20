using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPT373_AS2
{
    public partial class GolAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Server.Transfer("UserList.aspx");
            Response.Redirect("UserList.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Server.Transfer("TemplateList.aspx");
            Response.Redirect("TemplateList.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // Kill the session variables & return to login page
            Session["Username"] = null;
            Session["Name"] = null;

            //Server.Transfer("Login.aspx");
            Response.Redirect("Login.aspx");
        }
    }
}