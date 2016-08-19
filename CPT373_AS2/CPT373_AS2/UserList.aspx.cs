using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPT373_AS2
{
    public partial class UserList1 : System.Web.UI.Page
    {

        UsersManager usersManager = new UsersManager();
        DataTable usersTable = new DataTable();
        

        protected void Page_Load(object sender, EventArgs e)
        {

            usersTable = usersManager.GetUsers();

            UserListGridView.DataSource = usersTable;
            UserListGridView.DataBind();
        }

        protected void UserListGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void UserListGridView_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? id = null;
            var v = sender;

            var keys = e.Keys.Values;
            //var userID = keys.
            foreach (var item in keys)
            {
                id = (int)item;
            }
            usersManager.DeleteUser(id);
            usersTable = usersManager.GetUsers();
            UserListGridView.DataSource = usersTable;
            UserListGridView.DataBind();
        }
    }
}