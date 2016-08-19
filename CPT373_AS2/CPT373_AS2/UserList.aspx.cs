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
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersManagerDisconnected usersManager = new UsersManagerDisconnected();
            DataTable usersTable = usersManager.GetUsers();

            UserListGridView.DataSource = usersTable;
            UserListGridView.DataBind();
        }
    }
}