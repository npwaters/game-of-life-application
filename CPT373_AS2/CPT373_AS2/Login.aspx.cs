using CPT373_AS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPT373_AS2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            var email = Email.Text;
            var password = Password.Text;

            User user = new User { Email = email, Password = password };



            using (GOLDBEntities database = new GOLDBEntities())
            {
                // Retrieve a user with the same username and password.
                User login = database.Users.FirstOrDefault(u => u.Email == user.Email &&
                                                                u.Password == user.Password);

                // If successful set the session variables and go to Member page.
                if (login != null && login.IsAdmin)
                {
                    Session["Username"] = login.Email;
                    Session["Name"] = login.FirstName;

                    //LoginResult.Text = "Login successful!";
                    //Server.Transfer("UserList.aspx");
                    Response.Redirect("UserList.aspx");
                
                }

                // TODO:
                // redirect to a page


            }

        }
    }
}